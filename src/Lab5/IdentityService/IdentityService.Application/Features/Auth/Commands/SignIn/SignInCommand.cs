using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.Features.Auth.Commands.SignIn;

public record SignInCommand(
    [Required(ErrorMessage = "Username is required")]
    string Username,
    [Required(ErrorMessage = "Password is required")]
    string Password);