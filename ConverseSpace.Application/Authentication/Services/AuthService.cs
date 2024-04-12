using System.Text.RegularExpressions;
using AutoMapper;
using ConverseSpace.Application.Authentication.Commands.Register;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Auth;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Application.Authentication.Services;

public class AuthService(
    IPasswordHasher passwordHasher,
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository,
    IJwtProvider jwtProvider,
    IMapper mapper) : IAuthService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IRolesRepository _rolesRepository = rolesRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Register(string username, string email, string password)
    {

        if (!await сheckUsernameUniqueness(username))
            return Result.Failure(AuthErrors.UsernameExist);

        if (!await checkEmailUniqueness(email))
            return Result.Failure(AuthErrors.EmailExist);
        
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new User(username, email, hashedPassword);

        await _usersRepository.Add(user);

        return Result.Success();
    }

    public async Task<Result<string>> Login(string username, string password)
    {
        const string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

        User? user;

        if (Regex.IsMatch(username, pattern))
            user = await _usersRepository.GetByEmail(username);
        else
            user = await _usersRepository.GetByUsername(username);

        if (user is null)
            return Result<string>.Failure(AuthErrors.AccountNotFound);

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
            return Result<string>.Failure(AuthErrors.InvalidCredentials);

        var token = _jwtProvider.GenerateToken(user);

        return Result<string>.Success(token);
    }

    public async Task<string> CreateRole(string name)
    {
        var role = new Role { Name = name };
        await _rolesRepository.Add(role);

        return "Роль успешно создана";
    }

    private async Task<bool> сheckUsernameUniqueness(string username)
    {
        var user = await _usersRepository.GetByUsername(username);
        return user is null;
    }

    private async Task<bool> checkEmailUniqueness(string email)
    {
        var user = await _usersRepository.GetByEmail(email);
        return user is null;
    }
}