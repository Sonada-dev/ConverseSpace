using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Communities.Follows.Commands.Follow;

public record FollowCommand(Guid UserId, Guid CommunityId) : IRequest<Result>;

public class FollowCommandHandler(IFollowsService followsService) : IRequestHandler<FollowCommand, Result>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result> Handle(FollowCommand request, CancellationToken cancellationToken)
    {
        return await _followsService.Follow(request.UserId, request.CommunityId);
    }
}