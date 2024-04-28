using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Posts.Commands;

public record UploadResponse(Guid Id, string Path, MediaType Type);

public record CreatePostCommand() : IRequest<Result>
{
    [Required]
    public required string Title { get; init; }
    [StringLength(2048)] public string? ContentText { get; init; }
    [JsonIgnore]
    public Guid? CreatedBy { get; set; }
    [JsonIgnore]
    public Guid? Community { get; set; }
    public List<UploadResponse> Uploads = new List<UploadResponse>();
}

public class CreatePostCommandHandler(IMapper mapper, IPostsService postsService) : IRequestHandler<CreatePostCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPostsService _postsService = postsService;

    public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);
        var mediaContent = request.Uploads
            .Select(upload => new PostContentMedia(upload.Id, upload.Path, post.Id, upload.Type))
            .ToList();
        post.UpdateContentMedia(mediaContent);
        return await _postsService.CreatePost(post);
    }
}