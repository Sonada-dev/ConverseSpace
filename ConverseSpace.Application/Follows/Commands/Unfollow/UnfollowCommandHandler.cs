using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Follows.Commands.Unfollow;

public record UnfollowComamnd(Guid UserId, Guid ComamndId) : IRequest<Result>;

public class UnfollowCommandHandler(IFollowsService followsService) : IRequestHandler<UnfollowComamnd, Result>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result> Handle(UnfollowComamnd request, CancellationToken cancellationToken)
    {
        return await _followsService.Unfollow(request.UserId, request.ComamndId);
    }
}