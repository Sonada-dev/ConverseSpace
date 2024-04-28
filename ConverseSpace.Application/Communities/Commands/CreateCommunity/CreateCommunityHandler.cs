using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models;
using ConverseSpace.Domain.Models.Enums;
using MediatR;

namespace ConverseSpace.Application.Communities.Commands.CreateCommunity;

public record CreateCommunityRequest : IRequest<Result>
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Title { get; init; }
    [StringLength(512)]
    public string? Description { get; init; }
    [JsonIgnore]
    public Guid CreatedBy { get; set; }
    public bool IsPrivate { get; init; }
    public bool CheckPosts { get; init; }
    [EnumDataType(typeof(CommentsSettings))]
    public CommentsSettings Comments { get; init; }
}

public class CreateCommunityHandler(ICommunitiesService communitiesService, IUsersService usersService, IMapper mapper) : IRequestHandler<CreateCommunityRequest, Result>
{
    private readonly ICommunitiesService _communitiesService = communitiesService;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result> Handle(CreateCommunityRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersService.GetUserById(request.CreatedBy);
        if (user.Role == 2 && user.Communities.Count == 3)
            return Result.Failure(CommunitiesErrors.CommunityLimit);
        
        var community = _mapper.Map<Community>(request);
        return await _communitiesService.CreateCommunity(community);
    }
}


