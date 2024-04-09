using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Follows.Commands.Follow;

public record FollowComamnd(Guid UserId, Guid ComamndId) : IRequest<Result>;

public class FollowCommandHandler(IFollowsService followsService) : IRequestHandler<FollowComamnd, Result>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result> Handle(FollowComamnd request, CancellationToken cancellationToken)
    {
        return await _followsService.Follow(request.UserId, request.ComamndId);
    }
}