using ConverseSpace.Application.Authentication.Services;
using MediatR;

namespace ConverseSpace.Application.Authentication.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<string>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, string>
{   
    private readonly IAuthService _authService = authService;
    
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.Username, request.Password);
    }
}
