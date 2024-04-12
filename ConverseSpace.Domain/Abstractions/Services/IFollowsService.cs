using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface IFollowsService
{
    Task<Result> Follow(Guid userId, Guid communityId);
    Task<Result> Unfollow(Guid userId, Guid communityId);
    Task<Result<List<User>>> Followers(Guid communityId);
    Task<Result<List<JoinRequest>>> Requests(Guid communityId);
    Task<Result> AproveOrRejectRequest(Guid requestId, bool isAprove);
}