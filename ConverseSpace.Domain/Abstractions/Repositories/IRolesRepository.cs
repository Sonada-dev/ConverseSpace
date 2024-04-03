using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IRolesRepository
{
    Task Add(Role role);
}