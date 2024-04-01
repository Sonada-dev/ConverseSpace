using ConverseSpace.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConverseSpace.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(UsersService usersService) : ControllerBase
{
    private readonly UsersService _usersService = usersService;

    [HttpPost("register")]
    public async Task<string> Register([FromBody] RegisterResponse response)
    {
        return await _usersService.Register(response.Username, response.Email, response.Password);
    }

    [HttpPost("login")]
    public async Task<string> Login(string username, string password)
    {
        return await _usersService.Login(username, password);
    }

    [HttpPost("role")]
    public async Task<string> CreateRole([FromBody] string title)
    {
        return await _usersService.CreateRole(title);
    }

    public record RegisterResponse(string Username, string Email, string Password);

    public record LoginResponse(string Username, string Password);
}