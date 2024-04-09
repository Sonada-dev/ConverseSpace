using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using MediatR;

namespace ConverseSpace.Application.Users.Queries;

public record GetUsersRequest() : IRequest<List<User>>;

public class GetUsersHandler(IUsersService usersService) : IRequestHandler<GetUsersRequest, List<User>>
{
    private readonly IUsersService _usersService = usersService;
    
    public async Task<List<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return await _usersService.GetUsers();
    }
}