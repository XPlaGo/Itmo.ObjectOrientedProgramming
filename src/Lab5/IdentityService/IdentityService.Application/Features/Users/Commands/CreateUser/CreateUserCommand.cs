using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand
{
    public CreateUserCommand() { }

    public CreateUserCommand(
        string username,
        string password,
        bool isBlocked,
        UserRole role)
    {
        Username = username;
        Password = password;
        IsBlocked = isBlocked;
        Role = role;
    }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public UserRole Role { get; } = UserRole.User;
}