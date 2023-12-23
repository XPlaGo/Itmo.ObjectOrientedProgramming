using CurrencyService.Domain.Entities;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace CurrencyService.Persistence.Mapping;

public class CurrencyReader : IEntityReader<Currency>
{
    public Currency Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new Currency(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetDateTime(2),
            reader.GetDateTime(3));
    }
}