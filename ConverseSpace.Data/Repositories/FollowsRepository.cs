using AutoMapper;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class FollowsRepository(CSDBContext context, IMapper mapper) : IFollowsRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Add(Follow follower)
    {
        var followEntity = _mapper.Map<FollowEntity>(follower);
        await _context.Follows.AddAsync(followEntity);

        await _context.SaveChangesAsync();

        return Result.Success();
    }
    
    public async Task<Result> Delete(Guid followerId, Guid communityId)
    {
        var followEntity = await _context.Follows
            .FirstOrDefaultAsync(f => f.Follower == followerId && f.Community == communityId);

        if (followEntity is null)
            return Result.Failure(FollowsErrors.AlreadyUnfollow);
        
        _context.Follows.Remove(followEntity);
        
        await _context.SaveChangesAsync();
        
        return Result.Success();
    }
    
    public async Task<bool> IsUserCreator(Guid followId, Guid communityId)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Communities)
            .AnyAsync(u => u.Id == followId && u.Communities
                .Any(c => c.CreatedBy == communityId));
    }
    
    public async Task<bool> IsFollowExist(Guid followerId, Guid communityId) =>
        await _context.Follows
            .AsNoTracking()
            .AnyAsync(f => f.Follower == followerId && f.Community == communityId);
}