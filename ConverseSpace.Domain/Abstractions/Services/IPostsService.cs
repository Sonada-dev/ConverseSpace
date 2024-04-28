using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface IPostsService
{
    Task<Result<List<Post>>> GetPosts(Guid communityId, Guid userId);
    Task<Result> CreatePost(Post post);
    Task<Result> DeletePost(Guid postId);
}