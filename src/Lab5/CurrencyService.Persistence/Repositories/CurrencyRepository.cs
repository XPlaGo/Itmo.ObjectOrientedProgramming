using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Domain.Entities;
using CurrencyService.Persistence.Contexts;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace CurrencyService.Persistence.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    public CurrencyRepository(ApplicationDbContext dbContext, IEntityReader<Currency> entityReader, IEntityReader<long> idReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        EntityReader = entityReader;
        IdReader = idReader;
    }

    public ApplicationDbContext DbContext { get; }
    public IEntityReader<Currency> EntityReader { get; }
    public IEntityReader<long> IdReader { get; }

    public async Task<Currency?> GetByIdAsync(long currencyCode)
    {
        using var command = new NpgsqlCommand(
            "select currency_code, currency_name, created_date, updated_date from currencies where currency_code = @currency_code",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("currency_code", currencyCode);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<List<Currency>> GetAllAsync()
    {
        using var command = new NpgsqlCommand(
            "select currency_code, currency_name, created_date, updated_date from currencies",
            DbContext.Connection,
            DbContext.Transaction);
        return await ExecuteQueryableReaderAsync(command, EntityReader).ToListAsync().ConfigureAwait(false);
    }

    public async Task<long> AddAsync(Currency entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "insert into currencies (currency_name, created_date, updated_date)" +
            "values (@currency_name, @created_date, @updated_date)" +
            "returning currency_code",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("currency_name", entity.CurrencyName);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        return await ExecuteQueryableReaderAsync(command, IdReader).FirstAsync().ConfigureAwait(false);
    }

    public Task UpdateAsync(Currency entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Currency entity)
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