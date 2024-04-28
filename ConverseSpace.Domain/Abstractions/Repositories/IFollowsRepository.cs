using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IFollowsRepository
{
    Task<Result> Add(Follow follower);
    Task<Result> Delete(Guid followerId, Guid communityId);
    Task<bool> IsUserCreator(Guid followId, Guid communityId);
    Task<bool> IsFollowExist(Guid followerId, Guid communityId);
}