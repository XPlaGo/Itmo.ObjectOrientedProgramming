using Npgsql;

namespace TransactionService.Persistence.Mapping;

public interface IEntityReader<out T>
{
    public T Read(NpgsqlDataReader reader);
}