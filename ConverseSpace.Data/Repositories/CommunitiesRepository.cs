using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class CommunitiesRepository(CSDBContext context, IMapper mapper) : ICommunitiesRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;


    public async Task<List<Community>> Get() =>
        await _context.Communities
            .AsNoTracking()
            .Include(c => c.Follows)
            .Where(c => c.IsDeleted != true)
            .ProjectTo<Community>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<Community> GetByIdFull(Guid id)
    {
        var communityEntity = await _context.Communities
            .AsNoTracking()
            .Where(c => c.IsDeleted != true)
            .Include(c => c.Follows)
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Community>(communityEntity);
    }
    
    public async Task<Community> GetById(Guid id)
    {
        var communityEntity = await _context.Communities
            .AsNoTracking()
            .Where(c => c.IsDeleted != true)
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Community>(communityEntity);
    }

    public async Task Add(Community community)
    {
        var communityEntity = _mapper.Map<CommunityEntity>(community);
        await _context.Communities.AddAsync(communityEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Community community)
    {
        var communityEntity = _mapper.Map<CommunityEntity>(community);
        _context.Communities.Update(communityEntity);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> IsCommunityExist(Guid communityId) =>
        await _context.Communities.AnyAsync(c => c.Id == communityId);
}