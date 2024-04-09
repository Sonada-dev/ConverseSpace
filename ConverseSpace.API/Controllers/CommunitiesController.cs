using System.Security.Claims;
using ConverseSpace.Application;
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
            var communitites = await _mediator.Send(new GetCommunitiesRequest());
            if (communitites.Count == 0)
                return NoContent();
                
            return Ok(communitites);
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityRequest request)
        {
            var result = await _mediator.Send(request);
            
            if (result.StatusCode == 403)
                return StatusCode(403, result.Status);

            if(HandleException.CheckHandleException(result.Status))
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(Guid id)
        {
            var result = await _mediator.Send(new DeleteCommunityRequest(id));
            if(HandleException.CheckHandleException(result))
                return Ok(result);

            return StatusCode(500);
        }
    }
}
