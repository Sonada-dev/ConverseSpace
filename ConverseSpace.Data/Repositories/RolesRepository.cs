using AutoMapper;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Data.Repositories;

public class RolesRepository(CSDBContext context, IMapper mapper) : IRolesRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task Add(Role role)
    {
        var roleEntity = _mapper.Map<RoleEntity>(role);

        await _context.Roles.AddAsync(roleEntity);
        await _context.SaveChangesAsync();
    }
}