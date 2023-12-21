using Npgsql;

namespace TransactionService.Persistence.Mapping;

public class IdReader : IEntityReader<string>
{
    public string Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return reader.GetString(0);
    }
}