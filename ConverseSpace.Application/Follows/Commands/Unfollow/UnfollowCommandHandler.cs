using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Communities.Follows.Commands.Unfollow;

public record UnfollowCommand(Guid UserId, Guid CommunityId) : IRequest<Result>;

public class UnfollowCommandHandler(IFollowsService followsService) : IRequestHandler<UnfollowCommand, Result>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result> Handle(UnfollowCommand request, CancellationToken cancellationToken)
    {
        return await _followsService.Unfollow(request.UserId, request.CommunityId);
    }
}