using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;

namespace ConverseSpace.Application.Users.Services;

public class UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository) : IUsersService
{
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<string> AddRoleForUser(Guid userId, int roleId)
    {
        await _rolesRepository.AddRoleForUser(userId, roleId);
        return "Пользователю успешно добавлена роль";
    }
}