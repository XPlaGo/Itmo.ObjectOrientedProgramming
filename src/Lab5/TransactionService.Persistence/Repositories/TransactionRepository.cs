using Npgsql;
using TransactionService.Application.Exceptions;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Domain.Entities;
using TransactionService.Persistence.Contexts;
using TransactionService.Persistence.Mapping;

namespace TransactionService.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext dbContext, IEntityReader<Transaction> entityReader, IEntityReader<string> idReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        EntityReader = entityReader;
        IdReader = idReader;
    }

    public ApplicationDbContext DbContext { get; }
    public IEntityReader<Transaction> EntityReader { get; }
    public IEntityReader<string> IdReader { get; }

    public async Task<Transaction?> GetByTokenAsync(string token)
    {
        try
        {
            using var command = new NpgsqlCommand(
                "select transaction_token, from_account_id, from_account_type, from_user_id, " +
                "from_currency_code, from_amount, to_account_id, to_account_type, to_user_id, " +
                "to_currency_code, to_amount, created_date, updated_date from transactions " +
                "where transaction_token like @token ",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("token", token);
            return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (PostgresException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<IReadOnlyList<Transaction>> GetAllByUserId(long userId)
    {
        try
        {
            using var command = new NpgsqlCommand(
                "select transaction_token, from_account_id, from_account_type, from_user_id, " +
                "from_currency_code, from_amount, to_account_id, to_account_type, to_user_id, " +
                "to_currency_code, to_amount, created_date, updated_date from transactions " +
                "where from_user_id = @user_id",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("user_id", userId);
            return await ExecuteQueryableReaderAsync(command, EntityReader).ToListAsync().ConfigureAwait(false);
        }
        catch (PostgresException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<string> AddAsync(Transaction entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            using var command = new NpgsqlCommand(
                "insert into transactions (transaction_token, from_account_id, from_account_type, from_user_id, " +
                "from_currency_code, from_amount, to_account_id, to_account_type, to_user_id, " +
                "to_currency_code, to_amount, created_date, updated_date) values " +
                "(@transaction_token, @from_account_id, @from_account_type, @from_user_id," +
                "@from_currency_code, @from_amount, @to_account_id, @to_account_type, @to_user_id," +
                "@to_currency_code, @to_amount, @created_date, @updated_date) " +
                "returning transaction_token",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("transaction_token", entity.TransactionToken);
            command.Parameters.AddWithValue("from_account_id", entity.FromAccountId);
            command.Parameters.AddWithValue("from_account_type", entity.FromAccountType);
            command.Parameters.AddWithValue("from_user_id", entity.FromUserId);
            command.Parameters.AddWithValue("from_currency_code", entity.FromCurrencyCode);
            command.Parameters.AddWithValue("from_amount", entity.FromAmount);
            command.Parameters.AddWithValue("to_account_id", entity.ToAccountId);
            command.Parameters.AddWithValue("to_account_type", entity.ToAccountType);
            command.Parameters.AddWithValue("to_user_id", entity.ToUserId);
            command.Parameters.AddWithValue("to_currency_code", entity.ToCurrencyCode);
            command.Parameters.AddWithValue("to_amount", entity.ToAmount);
            command.Parameters.AddWithValue("created_date", entity.CreatedDate);
            command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
            return await ExecuteQueryableReaderAsync(command, IdReader).FirstAsync().ConfigureAwait(false);
        }
        catch (PostgresException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<string?> UpdateAsync(Transaction entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            using var command = new NpgsqlCommand(
                "update transactions set from_account_id = @from_account_id," +
                "from_account_type = @from_account_type, from_user_id = @from_user_id, " +
                "from_currency_code = @from_currency_code, from_amount = @from_amount, to_account_id = @to_account_id," +
                "to_account_type = @to_account_type, to_user_id = @to_user_id, to_currency_code = @to_currency_code," +
                "to_amount = @to_amount, created_date = @created_date, updated_date = @updated_date " +
                "where transaction_token like @transaction_token " +
                "returning transaction_token",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("transaction_token", entity.TransactionToken);
            command.Parameters.AddWithValue("from_account_id", entity.FromAccountId);
            command.Parameters.AddWithValue("from_account_type", entity.FromAccountType);
            command.Parameters.AddWithValue("from_user_id", entity.FromUserId);
            command.Parameters.AddWithValue("from_currency_code", entity.FromCurrencyCode);
            command.Parameters.AddWithValue("from_amount", entity.FromAmount);
            command.Parameters.AddWithValue("to_account_id", entity.ToAccountId);
            command.Parameters.AddWithValue("to_account_type", entity.ToAccountType);
            command.Parameters.AddWithValue("to_user_id", entity.ToUserId);
            command.Parameters.AddWithValue("to_currency_code", entity.ToCurrencyCode);
            command.Parameters.AddWithValue("to_amount", entity.ToAmount);
            command.Parameters.AddWithValue("created_date", entity.CreatedDate);
            command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
            return await ExecuteQueryableReaderAsync(command, IdReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (PostgresException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }

    public async Task<string?> DeleteAsync(string token)
    {
        ArgumentNullException.ThrowIfNull(token);

        try
        {
            using var command = new NpgsqlCommand(
                "delete from transactions where transaction_token like @token " +
                "returning transaction_token",
                DbContext.Connection,
                DbContext.Transaction);
            command.Parameters.AddWithValue("token", token);
            return await ExecuteQueryableReaderAsync(command, IdReader).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        catch (PostgresException exception)
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