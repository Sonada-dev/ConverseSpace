using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Abstractions.Repositories;
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
            .ProjectTo<Community>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task Add(Community community)
    {
        var communityEntity = _mapper.Map<CommunityEntity>(community);
        await _context.Communities.AddAsync(communityEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _context.Communities
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}