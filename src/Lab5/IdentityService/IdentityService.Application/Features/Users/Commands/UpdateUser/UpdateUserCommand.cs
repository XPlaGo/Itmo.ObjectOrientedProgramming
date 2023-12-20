namespace IdentityService.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
}