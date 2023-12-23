using IdentityService.Application.Features.Auth.Commands.SignIn;
using IdentityService.Application.Features.Auth.Commands.SignUp;
using IdentityService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInCommandHandler _signIn;
    private readonly SignUpCommandHandler _signUp;

    public AuthController(SignInCommandHandler signIn, SignUpCommandHandler signUp)
    {
        _signIn = signIn;
        _signUp = signUp;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("signin")]
    public async Task<ActionResult<Result<SignInTokenDto>>> Signin([FromBody] SignInCommand signInCommand)
    {
        Result<SignInTokenDto> result = await _signIn.Handle(signInCommand, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<Result<SignUpTokenDto>>> SignUp([FromBody] SignUpCommand signUpCommand)
    {
        Result<SignUpTokenDto> result = await _signUp.Handle(signUpCommand, default).ConfigureAwait(false);

        if (result.Succeeded is false) return BadRequest(result);

        return result;
    }
}