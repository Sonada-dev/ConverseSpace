using ConverseSpace.Application.Posts.Commands;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Posts.Services;

public class PostsService(IPostsRepository postsRepository) : IPostsService
{
    private readonly IPostsRepository _postsRepository = postsRepository;

    public async Task<Result<List<Post>>> GetPosts() =>
        await _postsRepository.Get();

    public async Task<Result> CreatePost(Post post)
    {
        await _postsRepository.Add(post);

        return Result.Success();
    }
}