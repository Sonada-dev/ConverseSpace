using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Posts.Queries;

public record GetPostsRequest() : IRequest<List<GetPostsResponse>>;

public class GetPostsHandler(IPostsService postsService, IMapper mapper) : IRequestHandler<GetPostsRequest, List<GetPostsResponse>>
{
    private readonly IPostsService _postsService = postsService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<GetPostsResponse>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var posts = await _postsService.GetPosts();
        return _mapper.Map<List<Post>,List<GetPostsResponse>>(posts);
    }
}

public record GetPostsResponse()
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? ContentText { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid CreatedBy { get; init; }
    public StatusPost Status { get; init; }
    public List<PostContentMedia> PostContentMedia { get; init; } = [];
}

