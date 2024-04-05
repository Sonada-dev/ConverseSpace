namespace ConverseSpace.Domain.Abstractions.Services;

public interface IUsersService
{
    Task<string> AddRoleForUser(Guid userId, int roleId);
}