using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Posts.Queries;

public record GetPostsRequest(Guid CommunityId, Guid UserId) : IRequest<Result<List<GetPostsResponse>>>;

public class GetPostsHandler(IPostsService postsService, IMapper mapper) : IRequestHandler<GetPostsRequest, Result<List<GetPostsResponse>>>
{
    private readonly IPostsService _postsService = postsService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<GetPostsResponse>>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var result = await _postsService.GetPosts(request.CommunityId, request.UserId);
        if (result.IsFailure)
            return Result<List<GetPostsResponse>>.Failure(result.Error);
        
        var posts = _mapper.Map<List<Post>,List<GetPostsResponse>>(result.Value);

        return Result<List<GetPostsResponse>>.Success(posts);
    }
}

public record GetPostsResponse()
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? ContentText { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid Community { get; init; }
    public StatusPost Status { get; init; }
    public List<PostContentMedia> PostContentMedia { get; init; } = [];
}

