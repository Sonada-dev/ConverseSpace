using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class JoinRequestsRepository(CSDBContext context, IMapper mapper) : IJoinRequestsRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<JoinRequest>>> Get(Guid communityId)
    {
        var requests = await _context.JoinRequests
            .AsNoTracking()
            .ProjectTo<JoinRequest>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<JoinRequest>>.Success(requests);
    }

    public async Task<Result<JoinRequest>> GetById(Guid requestId)
    {
        var request = await _context.JoinRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == requestId);

        return Result<JoinRequest>.Success(_mapper.Map<JoinRequest>(request));
    }
    
    public async Task<Result> Add(JoinRequest request)
    {
        var requestEntity = _mapper.Map<JoinRequestEntity>(request);
        await _context.JoinRequests.AddAsync(requestEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> Update(JoinRequest request)
    {
        var requestEntity = _mapper.Map<JoinRequestEntity>(request);
        _context.JoinRequests.Update(requestEntity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}