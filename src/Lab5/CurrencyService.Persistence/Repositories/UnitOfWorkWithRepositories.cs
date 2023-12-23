using System.Data;
using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Persistence.Contexts;
using Npgsql;

namespace CurrencyService.Persistence.Repositories;

public class UnitOfWorkWithRepositories : IUnitOfWorkWithRepositories
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWorkWithRepositories(ApplicationDbContext dbContext, ICurrencyRepository currencyRepository, ICurrencyExchangeRepository currencyExchangeRepository)
    {
        _dbContext = dbContext;
        CurrencyRepository = currencyRepository;
        CurrencyExchangeRepository = currencyExchangeRepository;
    }

    public ICurrencyRepository CurrencyRepository { get; }

    public ICurrencyExchangeRepository CurrencyExchangeRepository { get; }

    public async ValueTask CommitAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        await _dbContext.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IUnitOfWorkWithRepositories> ConnectAndBeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        await OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
        return this;
    }

    public async Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<NpgsqlTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        return await _dbContext.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        await _dbContext.RollbackAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}