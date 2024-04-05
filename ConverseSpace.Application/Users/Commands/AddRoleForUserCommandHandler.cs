using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Users.Commands;

public record AddRoleForUserCommand(Guid UserId, int RoleId) : IRequest<string>;

public class AddRoleForUserCommandHandler(IUsersService usersService) : IRequestHandler<AddRoleForUserCommand, string>
{
    private readonly IUsersService _usersService = usersService;
    
    public async Task<string> Handle(AddRoleForUserCommand request, CancellationToken cancellationToken)
    {
        return await _usersService.AddRoleForUser(request.UserId, request.RoleId);
    }
}