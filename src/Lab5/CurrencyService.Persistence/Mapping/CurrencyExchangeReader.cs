using CurrencyService.Domain.Entities;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace CurrencyService.Persistence.Mapping;

public class CurrencyExchangeReader : IEntityReader<CurrencyExchange>
{
    public CurrencyExchange Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new CurrencyExchange(
            reader.GetInt64(0),
            reader.GetInt64(1),
            reader.GetDecimal(2),
            reader.GetDateTime(3),
            reader.GetDateTime(4));
    }
}