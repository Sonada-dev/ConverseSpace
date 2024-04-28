using AutoMapper;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Follows.Commands.Follow;

public record FollowCommand(Guid Follower, Guid Community) : IRequest<Result>;

public class FollowCommandHandler(IFollowsService followsService, IMapper mapper) : IRequestHandler<FollowCommand, Result>
{
    private readonly IFollowsService _followsService = followsService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(FollowCommand request, CancellationToken cancellationToken)
    {
        var follow = _mapper.Map<Domain.Models.Follow>(request);
        return await _followsService.Follow(follow);
    }
}