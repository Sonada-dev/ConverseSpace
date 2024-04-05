using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.Data.Repositories;

public class RolesRepository(CSDBContext context, IMapper mapper) : IRolesRepository
{
    private readonly CSDBContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Role>> Get() =>
        await _context.Roles
            .ProjectTo<Role>(_mapper.ConfigurationProvider)
            .ToListAsync();

    
    public async Task Add(Role role)
    {
        var roleEntity = _mapper.Map<RoleEntity>(role);

        await _context.Roles.AddAsync(roleEntity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRoleForUser(Guid userId, int roleId)
    {
        await _context.AddUserRoleAsync(userId, roleId);
    }
}