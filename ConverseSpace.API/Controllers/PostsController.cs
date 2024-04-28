using ConverseSpace.API.Extensions;
using ConverseSpace.API.ViewModels;
using ConverseSpace.Application.Posts.Commands;
using ConverseSpace.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers
{
    [Route("api/communities/{community}/posts")]
    [ApiController]
    public class PostsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetPosts(Guid community)
        {
            var userId = HttpContext.GetUserId();
            
            var result = await _mediator.Send(new GetPostsRequest(community, userId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            if (result.Value.Count == 0)
                return NoContent();
            
            return Ok(result.Value);
        }
        
        [Authorize(Roles = "1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> CreatePost(Guid community, [FromForm] UploadViewModel viewModel)
        {
            viewModel.Command.Community = community;
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var command = viewModel.Command;
            var files = viewModel.Files;

            var userId = HttpContext.GetUserId();

            if (userId == Guid.Empty)
                return BadRequest("Невозможно получить идентификатор пользователя");

            command.CreatedBy = userId;

            if (files != null && files.Count > 0)
            {
                var resultFiles = UploadExtension.Upload(files);
                if (resultFiles.IsFailure)
                    return StatusCode((int)resultFiles.Error.Code!, resultFiles.Error.Description);

                foreach (var response in resultFiles.Value)
                {
                    command.Uploads.Add(response);
                }
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return Ok();
        }

        [Authorize(Roles = "1, 2")]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var result = await _mediator.Send(new DeletePostCommand(postId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return Ok();
        }
    }
}
