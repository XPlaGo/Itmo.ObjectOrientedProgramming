using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Domain.Entities;
using BankAccountService.Persistence.Contexts;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace BankAccountService.Persistence.Repositories;

public class DepositAccountRepository : IDepositAccountRepository
{
    public DepositAccountRepository(ApplicationDbContext dbContext, IEntityReader<DepositAccount> entityReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        EntityReader = entityReader;
    }

    public ApplicationDbContext DbContext { get; }
    public IEntityReader<DepositAccount> EntityReader { get; }

    public async Task<DepositAccount?> GetByIdAsync(long id)
    {
        using var command = new NpgsqlCommand(
            "select id, amount, user_id, currency_code, created_date, updated_date from deposit_accounts " +
            "where id = @id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("id", id);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<DepositAccount?> GetByIdAndUserIdAsync(long id, long userId)
    {
        using var command = new NpgsqlCommand(
            "select id, amount, user_id, currency_code, created_date, updated_date from deposit_accounts " +
            "where id = @id and user_id = @user_id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("user_id", userId);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<List<DepositAccount>> GetAllAsync()
    {
        using var command = new NpgsqlCommand(
            "select id, amount, user_id, currency_code, created_date, updated_date from deposit_accounts",
            DbContext.Connection,
            DbContext.Transaction);
        return await ExecuteQueryableReaderAsync(command, EntityReader).ToListAsync().ConfigureAwait(false);
    }

    public async Task<long> AddAsync(DepositAccount entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "insert into deposit_accounts (amount, user_id, currency_code, created_date, updated_date)" +
            "values (@amount, @user_id, @currency_code, @created_date, @updated_date)",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("amount", entity.Amount);
        command.Parameters.AddWithValue("user_id", entity.UserId);
        command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        return await ExecuteNonQueryAsync(command).ConfigureAwait(false);
    }

    public async Task UpdateAsync(DepositAccount entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "update deposit_accounts set amount = @amount, user_id = @user_id, currency_code = @currency_code, created_date = @created_date, updated_date = @updated_date " +
            "where id = @id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("id", entity.Id);
        command.Parameters.AddWithValue("amount", entity.Amount);
        command.Parameters.AddWithValue("user_id", entity.UserId);
        command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        await ExecuteNonQueryAsync(command).ConfigureAwait(false);
    }

    public async Task DeleteAsync(DepositAccount entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "delete from deposit_accounts " +
            "where id = @id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("id", entity.Id);
        command.Parameters.AddWithValue("amount", entity.Amount);
        command.Parameters.AddWithValue("user_id", entity.UserId);
        command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        await ExecuteNonQueryAsync(command).ConfigureAwait(false);
    }

    private static async Task<long> ExecuteNonQueryAsync(NpgsqlCommand command)
    {
        return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    private static async Task<long> ExecuteScalarAsync(NpgsqlCommand command)
    {
        return (long)(await command.ExecuteScalarAsync().ConfigureAwait(false) ??
                      throw new InvalidOperationException());
    }

    private static async IAsyncEnumerable<TEntity> ExecuteQueryableReaderAsync<TEntity>(
        NpgsqlCommand command,
        IEntityReader<TEntity> entityReader)
    {
        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
        {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                yield return entityReader.Read(reader);
            }
        }
    }
}