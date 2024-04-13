using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class PostsRepository(CSDBContext context, IMapper mapper) : IPostsRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<Post>>> Get()
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.PostContentMedia)
            .ProjectTo<Post>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<Post>>.Success(posts);
    }

    public async Task<Result> Add(Post post)
    {
        var postEntity = _mapper.Map<PostEntity>(post);
        await _context.Posts.AddAsync(postEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}