namespace IdentityService.Application.Features.Auth.Commands.SignUp;

public class SignUpTokenDto
{
    public SignUpTokenDto(string idToken, string accessToken)
    {
        IdToken = idToken;
        AccessToken = accessToken;
    }

    public string IdToken { get; set; }
    public string AccessToken { get; set; }
}