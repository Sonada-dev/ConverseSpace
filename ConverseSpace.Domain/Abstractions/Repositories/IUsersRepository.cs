using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<List<User>> Get();
    Task<User> GetById(Guid id);
    Task<User> GetByIdFull(Guid id);
    Task<bool> IsUserExist(Guid id);
    Task Add(User user);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByUsername(string username);
}