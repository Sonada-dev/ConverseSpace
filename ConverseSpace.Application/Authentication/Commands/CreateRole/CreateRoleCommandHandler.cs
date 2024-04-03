using ConverseSpace.Application.Authentication.Services;
using MediatR;

namespace ConverseSpace.Application.Authentication.Commands.CreateRole;

public record CreateRoleCommand(string Name) : IRequest<string>;

public class CreateRoleCommandHandler(IAuthService authService) : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IAuthService _authService = authService;
    
    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return await _authService.CreateRole(request.Name);
    }
}