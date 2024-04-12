using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Follows.Commands.AproveRequest;

public record AproveRequestQuery(Guid RequestId) : IRequest<Result>;

public class AproveRequestHandler(IFollowsService followsService) : IRequestHandler<AproveRequestQuery, Result>
{
    private readonly IFollowsService _followsService = followsService;

    public async Task<Result> Handle(AproveRequestQuery request, CancellationToken cancellationToken)
    {
        return await _followsService.AproveOrRejectRequest(request.RequestId, true);
    }
}