using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Communities.Commands.DeleteCommunity;

public record DeleteCommunityRequest(Guid Id) : IRequest<string>;

public class DeleteCommunityHandler(ICommunitiesService communitiesService) : IRequestHandler<DeleteCommunityRequest, string>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    
    public async Task<string> Handle(DeleteCommunityRequest request, CancellationToken cancellationToken)
    {
        return await _communitiesService.DeleteCommunity(request.Id);
    }
}