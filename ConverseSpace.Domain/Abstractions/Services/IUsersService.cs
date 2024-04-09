using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Services;

public interface IUsersService
{
    Task<List<User>> GetUsers();
    Task<User> GetUserById(Guid id);
}