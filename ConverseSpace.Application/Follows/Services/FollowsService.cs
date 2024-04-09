using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
// ReSharper disable All

namespace ConverseSpace.Application.Follows.Services;

public class FollowsService(ICommunitiesRepository communitiesRepository, IUsersRepository usersRepository) : IFollowsService
{
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<Result> Follow(Guid userId, Guid communityId)
    {
        var user = await _usersRepository.GetByIdFull(userId);

        if (user is null)
            return Result.Failure(FollowsErrors.UserNotFound);
        
        if (user.Communities.Any(c => c.Id == communityId))
            return Result.Failure(FollowsErrors.SameUser);
        
        var community = await _communitiesRepository.GetById(communityId);
        
        if (community is null)
            return Result.Failure(FollowsErrors.CommunityNotFound);
        
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

    public async Task<List<User>> Followers(Guid communityId)
    {
        var community = await _communitiesRepository.GetByIdFull(communityId);
        var followers = community.Followers.ToList();
        
        return followers;
    }
}