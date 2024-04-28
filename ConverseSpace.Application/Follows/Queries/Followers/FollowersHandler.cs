using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using MediatR;

namespace ConverseSpace.Application.Follows.Queries.Followers;

public record FollowersRequest(Guid CommunityId) : IRequest<Result<List<Follow>>>;

public class FollowersHandler(IFollowsService followsService) : IRequestHandler<FollowersRequest,Result<List<Follow>>>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result<List<Follow>>> Handle(FollowersRequest request, CancellationToken cancellationToken)
    {
        return await _followsService.Followers(request.CommunityId);
    }
}