using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Domain.Entities;
using BankAccountService.Persistence.Contexts;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace BankAccountService.Persistence.Repositories;

public class CardAccountRepository : ICardAccountRepository
{
    private readonly IEntityReader<CardAccount> _entityReader;
    private readonly IEntityReader<long> _idReader;
    public CardAccountRepository(ApplicationDbContext dbContext, IEntityReader<CardAccount> entityReader, IEntityReader<long> idReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        _entityReader = entityReader;
        _idReader = idReader;
    }

    public ApplicationDbContext DbContext { get; }

    public async Task<CardAccount?> GetByIdAsync(long id)
    {
        try
        {
            using var command = new NpgsqlCommand(
                "select id, amount, user_id, currency_code, created_date, updated_date from card_accounts " +
                "where id = @id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("id", id);
            return await ExecuteQueryableReaderAsync(command, _entityReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<CardAccount?> GetByIdAndUserIdAsync(long id, long userId)
    {
        try
        {
            using var command = new NpgsqlCommand(
                "select id, amount, user_id, currency_code, created_date, updated_date from card_accounts " +
                "where id = @id and user_id = @user_id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("user_id", userId);
            return await ExecuteQueryableReaderAsync(command, _entityReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<List<CardAccount>> GetAllAsync()
    {
        try
        {
            using var command = new NpgsqlCommand(
                "select id, amount, user_id, currency_code, created_date, updated_date from card_accounts",
                DbContext.Connection,
                DbContext.Transaction);
            return await ExecuteQueryableReaderAsync(command, _entityReader).ToListAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<long> AddAsync(CardAccount entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var command = new NpgsqlCommand(
                "insert into card_accounts (amount, user_id, currency_code, created_date, updated_date)" +
                "values (@amount, @user_id, @currency_code, @created_date, @updated_date) " +
                "returning id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("amount", entity.Amount);
            command.Parameters.AddWithValue("user_id", entity.UserId);
            command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
            command.Parameters.AddWithValue("created_date", entity.CreatedDate);
            command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
            return await ExecuteQueryableReaderAsync(command, _idReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<long> UpdateAsync(CardAccount entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var command = new NpgsqlCommand(
                "update card_accounts set amount = @amount, user_id = @user_id, currency_code = @currency_code, created_date = @created_date, updated_date = @updated_date " +
                "where id = @id " +
                "returning id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("id", entity.Id);
            command.Parameters.AddWithValue("amount", entity.Amount);
            command.Parameters.AddWithValue("user_id", entity.UserId);
            command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
            command.Parameters.AddWithValue("created_date", entity.CreatedDate);
            command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
            return await ExecuteQueryableReaderAsync(command, _idReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<long> DeleteAsync(CardAccount entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            using var command = new NpgsqlCommand(
                "delete from card_accounts " +
                "where id = @id " +
                "returning id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("id", entity.Id);
            command.Parameters.AddWithValue("amount", entity.Amount);
            command.Parameters.AddWithValue("user_id", entity.UserId);
            command.Parameters.AddWithValue("currency_code", entity.CurrencyCode);
            command.Parameters.AddWithValue("created_date", entity.CreatedDate);
            command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
            return await ExecuteQueryableReaderAsync(command, _idReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
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