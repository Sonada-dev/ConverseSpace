using ConverseSpace.Application.Follows.Commands.Follow;
using ConverseSpace.Application.Follows.Commands.Unfollow;
using ConverseSpace.Application.Follows.Queries.Followers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public async Task<IActionResult> Followers([FromQuery] FollowersRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.Count == 0)
                return NoContent();

            return Ok(result);
        }
        
        [Authorize(Roles = "1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> Follow([FromQuery] FollowComamnd comamnd)
        {
            var result = await _mediator.Send(comamnd);
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(201, "Успешная подписка на сообщество");
        }
        
        [Authorize(Roles = "1, 2, 3")]
        [HttpDelete]
        public async Task<IActionResult> Unfollow([FromQuery] UnfollowComamnd comamnd)
        {
            var result = await _mediator.Send(comamnd);
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(200, "Успешная отписка от сообщества");
        }

    }
}
