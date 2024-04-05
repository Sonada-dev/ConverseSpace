using ConverseSpace.Application.Authentication.Services;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Authentication.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<string>;

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService = authService;
    
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Register(request.Username, request.Email, request.Password);
    }
}