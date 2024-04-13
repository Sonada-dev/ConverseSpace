using ConverseSpace.API.Extensions;
using ConverseSpace.API.ViewModels;
using ConverseSpace.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _mediator.Send(new GetPostsRequest());
            return Ok(posts);
        }
        
        [Authorize(Roles = "1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] UploadViewModel viewModel)
        {
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
    }
}
