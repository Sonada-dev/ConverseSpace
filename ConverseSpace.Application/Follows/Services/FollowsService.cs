using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;

// ReSharper disable All

namespace ConverseSpace.Application.Follows.Services;

public class FollowsService(
    ICommunitiesRepository communitiesRepository,
    IUsersRepository usersRepository,
    IJoinRequestsRepository joinRequestsRepository,
    IFollowsRepository followsRepository) : IFollowsService
{
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IJoinRequestsRepository _joinRequestsRepository = joinRequestsRepository;
    private readonly IFollowsRepository _followsRepository = followsRepository;

    public async Task<Result<List<Follow>>> Followers(Guid communityId)
    {
        var community = await _communitiesRepository.GetByIdFull(communityId);
        if (community is null)
        {
            return Result<List<Follow>>.Failure(FollowsErrors.CommunityNotFound);
        }
        var followers = community.Follows.ToList();
        
        return Result<List<Follow>>.Success(followers) ;
    }
    
    public async Task<Result> Follow(Follow follow)
    {
        try
        {
            bool isUserExist = await _usersRepository.IsUserExist(follow.Follower);

            if (!isUserExist)
                return Result.Failure(FollowsErrors.UserNotFound);
        
            bool isUserCreator = await _followsRepository.IsUserCreator(follow.Follower, follow.Community);
        
            if (isUserCreator)
                return Result.Failure(FollowsErrors.SameUser);
        
            var community = await _communitiesRepository.GetById(follow.Community);
        
            if (community is null)
                return Result.Failure(FollowsErrors.CommunityNotFound);
            
            bool isFollowExist = await _followsRepository.IsFollowExist(follow.Follower, follow.Community);

            if (isFollowExist)
                return Result.Failure(FollowsErrors.AlreadyFollow);

            if (community.Private)
            {
                var request = new JoinRequest(follow.Follower, follow.Community);
                await _joinRequestsRepository.Add(request);

                return Result.Failure(FollowsErrors.Request);
            }
        
            return await _followsRepository.Add(follow);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(500, ex.Message));
        }

    }
    
    public async Task<Result> Unfollow(Guid followerId, Guid communityId)
    {
        var isUserExist = await _usersRepository.IsUserExist(followerId);

        if (!isUserExist)
            return Result.Failure(FollowsErrors.UserNotFound);
        
        var isUserCreator = await _followsRepository.IsUserCreator(followerId, communityId);
        
        if (isUserCreator)
            return Result.Failure(FollowsErrors.SameUser);
        
        bool isCommunityExist = await _communitiesRepository.IsCommunityExist(communityId);

        if (!isCommunityExist)
            return Result.Failure(FollowsErrors.CommunityNotFound);
        
        return await _followsRepository.Delete(followerId, communityId);
    }

    public async Task<Result<List<JoinRequest>>> Requests(Guid communityId) => 
        await _joinRequestsRepository.Get(communityId);

    public async Task<Result> AproveOrRejectRequest(Guid requestId, bool isAprove)
    {
        var result = await _joinRequestsRepository.GetById(requestId);

        if (result.Value is null)
            return Result.Failure(JoinRequestErrors.RequestNotFound);

        var request = result.Value;
        
        request.UpdateStaus(isAprove ? StatusRequest.Approved : StatusRequest.Rejected);

        await _joinRequestsRepository.Update(request);
        
        bool isCommunityExist = await _communitiesRepository.IsCommunityExist(request.Community);

        if (!isCommunityExist)
            return Result.Failure(FollowsErrors.CommunityNotFound);
        
        var follower = new Follow(request.User, request.Community);

        await _followsRepository.Add(follower);

        return Result.Success();
    }
    
    
}