using AutoMapper;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Communities.Commands.CreateCommunity;

public record CreateCommunityRequest : IRequest<CreateCommunityResponse>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public DateOnly CreatedAt { get; init; }
    public required Guid CreatedBy { get; init; }
    public bool IsPrivate { get; init; }
    public bool CheckPosts { get; init; }
    public CommentsSettings Comments { get; init; }
}

public class CreateCommunityHandler(ICommunitiesService communitiesService, IUsersService usersService, IMapper mapper) : IRequestHandler<CreateCommunityRequest, CreateCommunityResponse>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<CreateCommunityResponse> Handle(CreateCommunityRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersService.GetUserById(request.CreatedBy);
        if (user.Role == 2 && user.Communities.Count == 3)
            return new CreateCommunityResponse("Обычному пользователю можно создать лишь 3 сообщества", 403);
        
        var community = _mapper.Map<Community>(request);
        return new CreateCommunityResponse(await _communitiesService.CreateCommunity(community)) ;
    }
}
public record CreateCommunityResponse(string Status, int? StatusCode = null);

