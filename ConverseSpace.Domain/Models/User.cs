namespace ConverseSpace.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int Role { get; set; } = 2;
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public string Avatar { get; set; } = string.Empty;
}