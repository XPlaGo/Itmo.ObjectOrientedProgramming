using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.Features.Auth.Commands.SignUp;

public record SignUpCommand(
    [Required(ErrorMessage = "Username is required")]
    [MinLength(4, ErrorMessage = "Min username length is 4")]
    string Username,
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Min password length is 8")]
    string Password);