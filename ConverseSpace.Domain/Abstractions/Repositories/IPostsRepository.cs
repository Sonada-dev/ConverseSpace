using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IPostsRepository
{
    Task<bool> IsPostExist(Guid postId);
    Task<List<Post>> Get(Guid communityId);
    Task<Result> Add(Post post);
    Task<Result> Delete(Guid postId);
}