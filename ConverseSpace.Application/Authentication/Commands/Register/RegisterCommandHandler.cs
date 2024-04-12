using System.ComponentModel.DataAnnotations;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Abstractions.Services;
using MediatR;

namespace ConverseSpace.Application.Authentication.Commands.Register;

public record RegisterCommand : IRequest<Result>
{
    [Required]
    [StringLength(20, MinimumLength = 6)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$")]
    public required string Username { get; init; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; init; }
    
    [Required]
    [StringLength(32, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+]).*$")]
    public required string Password { get; init; }

}

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, Result>
{
    private readonly IAuthService _authService = authService;
    
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Register(request.Username, request.Email, request.Password);
    }
}