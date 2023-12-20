using BankAccountService.Domain.Entities;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace BankAccountService.Persistence.Mapping;

public class CardAccountReader : IEntityReader<CardAccount>
{
    public CardAccount Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new CardAccount(
            reader.GetInt64(0),
            reader.GetDecimal(1),
            reader.GetInt64(2),
            reader.GetInt64(3),
            reader.GetDateTime(4),
            reader.GetDateTime(5));
    }
}