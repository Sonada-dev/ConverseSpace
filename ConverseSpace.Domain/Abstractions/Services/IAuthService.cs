namespace ConverseSpace.Domain.Abstractions.Services;

public interface IAuthService
{
    Task<Result> Register(string requestUsername, string requestEmail, string requestPassword);
    Task<Result<string>> Login(string username, string password);
    Task<string> CreateRole(string name);
    
}