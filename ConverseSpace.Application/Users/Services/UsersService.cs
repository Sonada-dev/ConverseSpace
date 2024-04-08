using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Users.Services;

public class UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository) : IUsersService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    
    public async Task<List<User>> GetUsers() =>
        await _usersRepository.Get();
    
    public async Task<User> GetUserById(Guid id) =>
        await _usersRepository.GetById(id);
}