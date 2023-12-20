using IdentityService.Domain.Entities;
using Npgsql;

namespace IdentityService.Persistence.Mapping;

public class UserReader : IEntityReader<User>
{
    public User Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetBoolean(4),
            reader.GetFieldValue<UserRole>(5),
            reader.GetDateTime(6),
            reader.GetDateTime(6));
    }
}