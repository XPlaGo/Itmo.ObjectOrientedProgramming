namespace IdentityService.Application.Features.Auth.Commands.SignIn;

public class SignInTokenDto
{
    public SignInTokenDto(string idToken, string accessToken)
    {
        IdToken = idToken;
        AccessToken = accessToken;
    }

    public string IdToken { get; set; }
    public string AccessToken { get; set; }
}