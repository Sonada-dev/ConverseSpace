using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IUsersRepository
{
    Task Add(User user);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUsername(string username);
}