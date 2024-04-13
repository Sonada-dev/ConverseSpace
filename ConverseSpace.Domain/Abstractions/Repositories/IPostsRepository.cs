using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IPostsRepository
{
    Task<Result<List<Post>>> Get();
    Task<Result> Add(Post post);
}