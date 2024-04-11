using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Communities.Commands.UpdateCommunity;

public record UpdateCommunityRequest : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; init; }
    [StringLength(512)]
    public string? Description { get; init; }
    public bool IsPrivate { get; init; }
    public bool CheckPosts { get; init; }
    [EnumDataType(typeof(CommentsSettings))]
    public CommentsSettings Comments { get; init; }
}

public class UpdateCommunityCommandHandler(ICommunitiesService communitiesService, IMapper mapper) : IRequestHandler<UpdateCommunityRequest, Result>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        var community = _mapper.Map<Community>(request);
        return await _communitiesService.UpdateCommunity(community);
    }
}