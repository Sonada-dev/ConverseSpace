using ConverseSpace.Application.Authentication.Commands.CreateRole;
using ConverseSpace.Application.Authentication.Commands.Login;
using ConverseSpace.Application.Authentication.Commands.Register;
using ConverseSpace.Application.Users.Commands;
using ConverseSpace.Data;
using ConverseSpace.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConverseSpace.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IMediator mediator, CSDBContext context) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly CSDBContext _context = context;

    [HttpPost("register")]
    public async Task<string> Register([FromBody] RegisterCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("login")]
    public async Task<string> Login([FromBody] LoginCommand request)
    {
        string token = await _mediator.Send(request);

        string hex = "746F6B656E";
        
        HttpContext.Response.Cookies.Append(hex, token);

        return token;
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpGet("users")]
    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        return await _context.Users.AsNoTracking().Include(u => u.Roles).ToListAsync();
    }

    [Authorize(Policy = "UsersPolicy")]
    [HttpPost("{userId}/roles")]
    public async Task<string> AddRoleForUser(Guid userId, [FromBody] int roleId)
    {
        var request = new AddRoleForUserCommand(userId, roleId);
        return await _mediator.Send(request);
    }

    [Authorize(Policy="AdminPolicy")]
    [HttpPost("roles")]
    public async Task<string> CreateRole([FromBody] CreateRoleCommand request)
    {
        //await _context.Roles.ToListAsync();
        return await _mediator.Send(request);
    }
}