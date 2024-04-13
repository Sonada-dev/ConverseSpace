using ConverseSpace.Application.Posts.Commands;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Posts.Services;

public class PostsService(IPostsRepository postsRepository) : IPostsService
{
    private readonly IPostsRepository _postsRepository = postsRepository;

    public async Task<List<Post>> GetPosts() =>
        await _postsRepository.Get();

    public async Task<Result> CreatePost(Post post)
    {
        try
        {
            await _postsRepository.Add(post);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(500, ex.Message));
        }
    }
}