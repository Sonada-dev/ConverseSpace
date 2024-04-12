using System.ComponentModel.DataAnnotations;
using ConverseSpace.Application.Authentication.Services;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Authentication.Commands.Login;

public record LoginCommand : IRequest<Result<string>>
{
    [Required]
    public required string Username { get; init; }
    [Required]
    public required string Password { get; init; }
}

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, Result<string>>
{   
    private readonly IAuthService _authService = authService;
    
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.Username, request.Password);
    }
}
