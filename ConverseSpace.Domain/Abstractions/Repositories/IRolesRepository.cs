using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IRolesRepository
{
    Task<List<Role>> Get();
    Task Add(Role role);
    Task<Role> GetById(int id);
}