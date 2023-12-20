namespace IdentityService.Application.Features.Users.Queries.GetUserByUsername;

public class GetUserByUsernameQuery
{
    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }

    public string Username { get; set; }
}