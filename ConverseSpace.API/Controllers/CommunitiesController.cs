using System.Security.Claims;
using ConverseSpace.API.Extensions;
using ConverseSpace.Application.Communities.Commands.CreateCommunity;
using ConverseSpace.Application.Communities.Commands.DeleteCommunity;
using ConverseSpace.Application.Communities.Commands.UpdateCommunity;
using ConverseSpace.Application.Communities.Follows.Queries.JoinRequests;
using ConverseSpace.Application.Communities.Queries.GetCommunities;
using ConverseSpace.Application.Follows.Commands.AproveRequest;
using ConverseSpace.Application.Follows.Commands.Follow;
using ConverseSpace.Application.Follows.Commands.RejectRequest;
using ConverseSpace.Application.Follows.Commands.Unfollow;
using ConverseSpace.Application.Follows.Queries.Followers;
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
            var userId = HttpContext.GetUserId();

            if (userId == Guid.Empty)
                return BadRequest("Невозможно получить идентификатор пользователя");

            request.CreatedBy = userId;
            
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

            return NoContent();
        }

        [Authorize(Roles = "1, 2")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommunity(Guid id, [FromBody] UpdateCommunityRequest request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(200, "Сообщество обновлено");
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("{communityId}/followers")]
        public async Task<IActionResult> Followers(Guid communityId)
        {
            var result = await _mediator.Send(new FollowersRequest(communityId));

            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpPost("{communityId}/follow")]
        public async Task<IActionResult> Follow(Guid communityId)
        {
            var userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new FollowCommand(userId, communityId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(201, "Успешная подписка на сообщество");
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpDelete("{communityId}/unfollow")]
        public async Task<IActionResult> Unfollow(Guid communityId)
        {
            var userId = HttpContext.GetUserId();

            var result = await _mediator.Send(new UnfollowCommand(userId, communityId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return StatusCode(200, "Успешная отписка от сообщества");
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet("{communityId}/join-requests")]
        public async Task<IActionResult> Requests(Guid communityId)
        {
            var result = await _mediator.Send(new JoinRequestsQuery(communityId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return Ok(result.Value);
        }

        [Authorize(Roles = "1, 2")]
        [HttpPatch("join-requests/{requestId}/approve")]
        public async Task<IActionResult> ApproveRequest(Guid requestId)
        {
            var result = await _mediator.Send(new AproveRequestQuery(requestId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return Ok();
        }

        [Authorize(Roles = "1, 2")]
        [HttpPatch("join-requests/{requestId}/reject")]
        public async Task<IActionResult> RejectRequest(Guid requestId)
        {
            var result = await _mediator.Send(new RejectRequestQuery(requestId));
            if (result.IsFailure)
                return StatusCode((int)result.Error.Code!, result.Error.Description);

            return Ok();
        }
    }
}
