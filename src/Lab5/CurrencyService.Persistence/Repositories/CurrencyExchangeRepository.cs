using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Domain.Entities;
using CurrencyService.Persistence.Contexts;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace CurrencyService.Persistence.Repositories;

public class CurrencyExchangeRepository : ICurrencyExchangeRepository
{
    public CurrencyExchangeRepository(ApplicationDbContext dbContext, IEntityReader<CurrencyExchange> entityReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        EntityReader = entityReader;
    }

    public ApplicationDbContext DbContext { get; }
    public IEntityReader<CurrencyExchange> EntityReader { get; }

    public async Task<CurrencyExchange?> GetByCodesAsync(long fromCode, long toCode)
    {
        using var command = new NpgsqlCommand(
            "select currency_from, currency_to, exchange_rate, created_date, updated_date from currency_exchanges " +
            "where currency_from = @currency_from and currency_to = @currency_to",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("currency_from", fromCode);
        command.Parameters.AddWithValue("currency_to", toCode);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<List<CurrencyExchange>> GetAllAsync()
    {
        using var command = new NpgsqlCommand(
            "select currency_from, currency_to, exchange_rate, created_date, updated_date from currency_exchanges",
            DbContext.Connection,
            DbContext.Transaction);
        return await ExecuteQueryableReaderAsync(command, EntityReader).ToListAsync().ConfigureAwait(false);
    }

    public async Task<long> AddAsync(CurrencyExchange entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "insert into currency_exchanges (currency_from, currency_to, exchange_rate, created_date, updated_date)" +
            "values (@currency_from, @currency_to, @exchange_rate, @created_date, @updated_date)",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("currency_from", entity.CurrencyFrom);
        command.Parameters.AddWithValue("currency_to", entity.CurrencyTo);
        command.Parameters.AddWithValue("exchange_rate", entity.ExchangeRate);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        return await ExecuteNonQueryAsync(command).ConfigureAwait(false);
    }

    public Task UpdateAsync(CurrencyExchange entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CurrencyExchange entity)
    {
        throw new NotImplementedException();
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