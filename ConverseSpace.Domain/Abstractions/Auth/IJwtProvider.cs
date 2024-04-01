using ConverseSpace.Domain.Models;

namespace ConverseSpace.Domain.Abstractions.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}