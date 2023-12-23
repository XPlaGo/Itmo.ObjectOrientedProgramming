using Npgsql;
using TransactionService.Domain.Entities;

namespace TransactionService.Persistence.Mapping;

public class TransactionReader : IEntityReader<Transaction>
{
    public Transaction Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new Transaction(
            reader.GetString(0),
            reader.GetInt64(1),
            reader.GetString(2),
            reader.GetInt64(3),
            reader.GetInt64(4),
            reader.GetDecimal(5),
            reader.GetInt64(6),
            reader.GetString(7),
            reader.GetInt64(8),
            reader.GetInt64(9),
            reader.GetDecimal(10),
            reader.GetDateTime(11),
            reader.GetDateTime(12));
    }
}