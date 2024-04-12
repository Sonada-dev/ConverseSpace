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
    IJoinRequestsRepository joinRequestsRepository) : IFollowsService
{
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IJoinRequestsRepository _joinRequestsRepository = joinRequestsRepository;

    public async Task<Result<List<User>>> Followers(Guid communityId)
    {
        var community = await _communitiesRepository.GetByIdFull(communityId);
        if (community is null)
        {
            return Result<List<User>>.Failure(FollowsErrors.CommunityNotFound);
        }
        var followers = community.Followers.ToList();
        
        return Result<List<User>>.Success(followers) ;
    }
    
    public async Task<Result> Follow(Guid userId, Guid communityId)
    {
        var community = await _communitiesRepository.GetById(communityId);
        
        if (community is null)
            return Result.Failure(FollowsErrors.CommunityNotFound);

        if (community.Private)
        {
            var request = new JoinRequest(userId, communityId);
            await _joinRequestsRepository.Add(request);

            return Result.Failure(FollowsErrors.Request);
        }
        
        var user = await _usersRepository.GetById(userId);

        if (user is null)
            return Result.Failure(FollowsErrors.UserNotFound);
        
        if (user.Communities.Any(c => c.Id == communityId))
            return Result.Failure(FollowsErrors.SameUser);
        
        var result = community.AddFollower(user);

        if (!result.IsSuccess)
            return result;
        
        await _communitiesRepository.Update(community);
        return Result.Success();
    }
    
    public async Task<Result> Unfollow(Guid userId, Guid communityId)
    {
        var user = await _usersRepository.GetByIdFull(userId);

        if (user is null)
            return Result.Failure(FollowsErrors.UserNotFound);
        
        if (user.Communities.Any(c => c.Id == communityId))
            return Result.Failure(FollowsErrors.SameUser);
        
        var community = await _communitiesRepository.GetByIdFull(communityId);

        if (community is null)
            return Result.Failure(FollowsErrors.CommunityNotFound);
        
        var result = community.RemoveFollower(user.Id);

        if (!result.IsSuccess)
            return result;
        
        await _communitiesRepository.Unfollow(communityId, userId);
        return Result.Success();
    }

    public async Task<Result<List<JoinRequest>>> Requests(Guid communityId) => 
        await _joinRequestsRepository.Get(communityId);

    public async Task<Result> AproveOrRejectRequest(Guid requestId, bool isAprove)
    {
        var result = await _joinRequestsRepository.GetById(requestId);

        if (result.Value is null)
            return Result.Failure(JoinRequestError.RequestNotFound);

        var request = result.Value;
        
        request.UpdateStaus(isAprove ? StatusRequest.Approved : StatusRequest.Rejected);

        await _joinRequestsRepository.Update(request);

        return Result.Success();
    }
}