using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Posts.Commands;

public record DeletePostCommand(Guid PostId) : IRequest<Result>;

public class DeletePostCommandHandler(IPostsService postsService) : IRequestHandler<DeletePostCommand, Result>
{
    private readonly IPostsService _postsService = postsService;

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        return await _postsService.DeletePost(request.PostId);
    }
}