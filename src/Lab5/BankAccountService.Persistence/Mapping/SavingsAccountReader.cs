using BankAccountService.Domain.Entities;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace BankAccountService.Persistence.Mapping;

public class SavingsAccountReader : IEntityReader<SavingsAccount>
{
    public SavingsAccount Read(NpgsqlDataReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return new SavingsAccount(
            reader.GetInt64(0),
            reader.GetDecimal(1),
            reader.GetInt64(2),
            reader.GetInt64(3),
            reader.GetDateTime(4),
            reader.GetDateTime(5));
    }
}