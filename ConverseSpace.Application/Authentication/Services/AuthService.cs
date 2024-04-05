using System.Text.RegularExpressions;
using ConverseSpace.Domain.Abstractions.Auth;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Authentication.Services;

public class AuthService(
    IPasswordHasher passwordHasher,
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository,
    IJwtProvider jwtProvider) : IAuthService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IRolesRepository _rolesRepository = rolesRepository;

    public async Task<string> Register(string username, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = hashedPassword
        };

        await _usersRepository.Add(user);

        return "Пользователь успешно зарегистрирован";
    }

    public async Task<string> Login(string username, string password)
    {
        const string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

        User? user;

        if (Regex.IsMatch(username, pattern))
            user = await _usersRepository.GetUserByEmail(username);
        else
            user = await _usersRepository.GetUserByUsername(username);

        if (user is null)
            return "Пользователь не найден.";

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
            return "Ошибка авторизации";

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<string> CreateRole(string name)
    {
        var role = new Role { Name = name };
        await _rolesRepository.Add(role);

        return "Роль успешно создана";
    }
}