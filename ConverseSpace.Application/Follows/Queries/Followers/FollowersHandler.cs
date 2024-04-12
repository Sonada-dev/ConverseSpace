using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using ConverseSpace.Domain.Models;
using MediatR;

namespace ConverseSpace.Application.Communities.Follows.Queries.Followers;

public record FollowersRequest(Guid CommunityId) : IRequest<Result<List<User>>>;

public class FollowersHandler(IFollowsService followsService) : IRequestHandler<FollowersRequest,Result<List<User>>>
{
    private readonly IFollowsService _followsService = followsService;
    
    public async Task<Result<List<User>>> Handle(FollowersRequest request, CancellationToken cancellationToken)
    {
        return await _followsService.Followers(request.CommunityId);
    }
    
    public record FollowersResponse
    {
        public Guid Id { get; init; } 
        public required string Username { get; init; }
        public string? Description { get; init; }
        public required string Email { get; init; }
        public DateOnly CreatedAt { get; init; } 
        public required string Avatar { get; init; }
        public int Role { get; init; } 
        //public ICollection<Comment> Comments { get; init; }
        //public ICollection<Post> Posts { get; init; }
        public ICollection<Community> Communities { get; init; } = [];
    }
}