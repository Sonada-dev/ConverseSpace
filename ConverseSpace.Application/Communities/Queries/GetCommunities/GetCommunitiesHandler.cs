using AutoMapper;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using MediatR;

namespace ConverseSpace.Application.Communities.Queries.GetCommunities;

public record GetCommunitiesRequest : IRequest<List<GetCommunityResponse>>;

public class GetCommunitiesHandler(ICommunitiesService communitiesService, IMapper mapper)
    : IRequestHandler<GetCommunitiesRequest, List<GetCommunityResponse>>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<GetCommunityResponse>> Handle(GetCommunitiesRequest request,
        CancellationToken cancellationToken)
    {
        var communities = await _communitiesService.GetCommunities();
        return _mapper.Map<List<Community>, List<GetCommunityResponse>>(communities);
    }
}


public record GetCommunityResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public DateOnly CreatedAt { get; init; }
    public required Guid CreatedBy { get; init; }
    public bool Private { get; init; }
    public bool CheckPosts { get; init; }
    public string? Comments { get; init; }
    public int FollowersCount { get; init; }
    public ICollection<CommunityTag> Tags { get; init; } = [];
}

public record Follower
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
}

