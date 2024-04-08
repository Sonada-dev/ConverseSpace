using ConverseSpace.Application.Authentication.Commands.CreateRole;
using ConverseSpace.Application.Authentication.Commands.Login;
using ConverseSpace.Application.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<string> Register([FromBody] RegisterCommand request) => 
        await _mediator.Send(request);

    [HttpPost("login")]
    public async Task<string> Login([FromBody] LoginCommand request)
    {
        string token = await _mediator.Send(request);

        string hex = "746F6B656E";
        
        HttpContext.Response.Cookies.Append(hex, token);

        return token;
    }

    [Authorize(Roles = "1")]
    [HttpPost("roles")]
    public async Task<string> CreateRole([FromBody] CreateRoleCommand request) => 
        await _mediator.Send(request);
}