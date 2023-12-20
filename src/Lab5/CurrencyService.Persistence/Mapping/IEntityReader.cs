using Npgsql;

namespace IdentityService.Persistence.Mapping;

public interface IEntityReader<T>
{
    public T Read(NpgsqlDataReader reader);
}