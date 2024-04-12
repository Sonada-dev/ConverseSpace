using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using MediatR;

namespace ConverseSpace.Application.Communities.Follows.Queries.JoinRequests;

public record JoinRequestsQuery(Guid CommunityId) : IRequest<Result<List<JoinRequest>>>;

public class JoinRequestsHandler(IFollowsService followsService) : IRequestHandler<JoinRequestsQuery, Result<List<JoinRequest>>>
{
    private readonly IFollowsService _followsService = followsService;

    public async Task<Result<List<JoinRequest>>> Handle(JoinRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _followsService.Requests(request.CommunityId);
    }
}