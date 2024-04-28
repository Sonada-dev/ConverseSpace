using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Posts.Services;

public class PostsService(
    IPostsRepository postsRepository,
    ICommunitiesRepository communitiesRepository) : IPostsService
{
    private readonly IPostsRepository _postsRepository = postsRepository;
    private readonly ICommunitiesRepository _communitiesRepository = communitiesRepository;

    public async Task<Result<List<Post>>> GetPosts(Guid communityId, Guid userId)
    {
        var community = await _communitiesRepository.GetByIdFull(communityId);

        if (community is null)
            return Result<List<Post>>.Failure(CommunitiesErrors.CommunityNotFound);

        bool isUserFollowed = community.Follows.Any(f => f.Id == userId);
        
        if (community.Private && !isUserFollowed)
            return Result<List<Post>>.Failure(PostsErrors.NotFollower);
        
        var posts = await _postsRepository.Get(communityId);
        
        return Result<List<Post>>.Success(posts);
    }

    public async Task<Result> CreatePost(Post post)
    {
        try
        {
            var community = await _communitiesRepository.GetByIdFull(post.Community);
            
            if (community is null)
                return Result<List<Post>>.Failure(CommunitiesErrors.CommunityNotFound);

            bool isUserFollowed = community.Follows.Any(f => f.Id == post.CreatedBy);
        
            if (community.Private && !isUserFollowed)
                return Result<List<Post>>.Failure(PostsErrors.NotFollower);
            
            await _postsRepository.Add(post);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(500, ex.Message));
        }
    }

    public async Task<Result> DeletePost(Guid postId)
    {
        return await _postsRepository.Delete(postId);
    }
    
}