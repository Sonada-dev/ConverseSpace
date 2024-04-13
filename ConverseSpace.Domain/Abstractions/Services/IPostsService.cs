using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface IPostsService
{
    Task<List<Post>> GetPosts();
    Task<Result> CreatePost(Post post);
}