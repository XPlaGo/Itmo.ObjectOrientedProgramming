namespace IdentityService.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery
{
    public GetUserByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}