using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class PostsRepository(CSDBContext context, IMapper mapper) : IPostsRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> IsPostExist(Guid postId)
    {
        return await _context.Posts.AnyAsync(p => p.Id == postId);
    }
    
    public async Task<List<Post>> Get(Guid communityId)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Where(p => p.Community == communityId && p.IsDeleted == false)
            .Include(p => p.PostContentMedia)
            .ProjectTo<Post>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return posts;
    }

    public async Task<Result> Add(Post post)
    {
        var postEntity = _mapper.Map<PostEntity>(post);
        await _context.Posts.AddAsync(postEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> Delete(Guid postId)
    {
        var postEntity = await _context.Posts.FindAsync(postId);

        if (postEntity == null || postEntity.IsDeleted == true)
            return Result.Failure(PostsErrors.PostNotFound);

        postEntity.IsDeleted = true;
        await _context.SaveChangesAsync();

        return Result.Success();
    }
    
}