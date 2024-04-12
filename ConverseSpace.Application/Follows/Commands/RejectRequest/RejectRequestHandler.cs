using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Follows.Commands.RejectRequest;

public record RejectRequestQuery(Guid RequestId) : IRequest<Result>;

public class RejectRequestHandler(IFollowsService followsService) : IRequestHandler<RejectRequestQuery, Result>
{
    private readonly IFollowsService _followsService = followsService;

    public async Task<Result> Handle(RejectRequestQuery request, CancellationToken cancellationToken)
    {
        return await _followsService.AproveOrRejectRequest(request.RequestId, false);
    }
}