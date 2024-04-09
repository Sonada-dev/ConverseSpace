using ConverseSpace.Application.Communities.Commands.CreateCommunity;
using ConverseSpace.Application.Communities.Commands.DeleteCommunity;
using ConverseSpace.Application.Communities.Queries.GetCommunities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet]
        public async Task<IActionResult> GetCommunities()
        {
            var communities = await _mediator.Send(new GetCommunitiesRequest());
            if (communities.Count == 0)
                return NoContent();
                
            return Ok(communities);
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(201, "Сообщество создано");
        }

        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(Guid id)
        {
            var result = await _mediator.Send(new DeleteCommunityRequest(id));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(204, "Сообщество удалено");
        }
    }
}
