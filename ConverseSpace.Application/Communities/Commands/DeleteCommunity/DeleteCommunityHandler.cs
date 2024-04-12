using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Communities.Commands.DeleteCommunity;

public record DeleteCommunityRequest(Guid Id) : IRequest<Result>;

public class DeleteCommunityHandler(ICommunitiesService communitiesService) : IRequestHandler<DeleteCommunityRequest, Result>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    
    public async Task<Result> Handle(DeleteCommunityRequest request, CancellationToken cancellationToken)
    {
        return await _communitiesService.DeleteCommunity(request.Id);
    }
}