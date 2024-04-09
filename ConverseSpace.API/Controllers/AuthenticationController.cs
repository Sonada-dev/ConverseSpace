using ConverseSpace.Application.Authentication.Commands.CreateRole;
using ConverseSpace.Application.Authentication.Commands.Login;
using ConverseSpace.Application.Authentication.Commands.Register;
using ConverseSpace.Domain;
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
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _mediator.Send(request);
        if (result.IsFailure)
            return StatusCode((int)result.Error.Code!, result.Error.Description);

        return StatusCode(201, "Пользователь зарегистрирован");
    }
        

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _mediator.Send(request);
        if (result.IsFailure)
            return StatusCode((int)result.Error.Code!, result.Error.Description);

        string token = result.Value;
        
        string hex = "746F6B656E";
        
        HttpContext.Response.Cookies.Append(hex, token);

        return StatusCode(200, token);
    }

    [Authorize(Roles = "1")]
    [HttpPost("roles")]
    public async Task<string> CreateRole([FromBody] CreateRoleCommand request) => 
        await _mediator.Send(request);
}