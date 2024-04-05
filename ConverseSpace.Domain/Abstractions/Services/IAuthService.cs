namespace ConverseSpace.Domain.Abstractions.Services;

public interface IAuthService
{
    Task<string> Register(string username, string email, string password);
    Task<string> Login(string username, string password);
    Task<string> CreateRole(string name);
}