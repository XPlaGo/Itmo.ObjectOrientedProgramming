using System.Data;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Persistence.Contexts;
using Npgsql;

namespace BankAccountService.Persistence.Repositories;

public class UnitOfWorkWithRepositories : IUnitOfWorkWithRepositories
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWorkWithRepositories(
        ApplicationDbContext dbContext,
        ICardAccountRepository cardAccountRepository,
        ISavingsAccountRepository savingsAccountRepository,
        IDepositAccountRepository depositAccountRepository)
    {
        _dbContext = dbContext;
        CardAccountRepository = cardAccountRepository;
        SavingsAccountRepository = savingsAccountRepository;
        DepositAccountRepository = depositAccountRepository;
    }

    public ICardAccountRepository CardAccountRepository { get; }
    public ISavingsAccountRepository SavingsAccountRepository { get; }
    public IDepositAccountRepository DepositAccountRepository { get; }

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