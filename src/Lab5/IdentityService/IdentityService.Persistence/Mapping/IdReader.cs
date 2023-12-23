using Npgsql;

namespace IdentityService.Persistence.Mapping;

public class IdReader : IEntityReader<long>
{
    public long Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return reader.GetInt64(0);
    }
}