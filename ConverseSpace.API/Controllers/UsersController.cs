using ConverseSpace.Application.Users.Queries;
using ConverseSpace.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _mediator.Send(new GetUsersRequest());
    }
}
