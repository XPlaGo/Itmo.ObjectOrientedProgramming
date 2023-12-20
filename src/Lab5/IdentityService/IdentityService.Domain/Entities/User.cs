using IdentityService.Domain.Common;

namespace IdentityService.Domain.Entities;

public class User : BaseMetaInfoEntity
{
    public User(string username, string password, string salt, bool isBlocked, UserRole role, DateTime createdDate, DateTime updatedDate)
    {
        Username = username;
        Password = password;
        Salt = salt;
        IsBlocked = isBlocked;
        Role = role;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    public User(long id, string username, string password, string salt, bool isBlocked, UserRole role, DateTime createdDate, DateTime updatedDate)
    {
        Id = id;
        Username = username;
        Password = password;
        Salt = salt;
        IsBlocked = isBlocked;
        Role = role;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool IsBlocked { get; set; }
    public UserRole Role { get; }
}