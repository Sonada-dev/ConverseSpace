namespace ConverseSpace.Domain.Abstractions.Services;

public interface IAuthService
{
    Task<string> Register(string requestUsername, string requestEmail, string requestPassword);
    Task<string> Login(string username, string password);
    Task<string> CreateRole(string name);
    
}