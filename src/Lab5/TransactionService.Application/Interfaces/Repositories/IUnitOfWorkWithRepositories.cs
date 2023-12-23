using System.Data;

namespace TransactionService.Application.Interfaces.Repositories;

public interface IUnitOfWorkWithRepositories : IDisposable
{
    ITransactionRepository TransactionRepository { get; }

    ValueTask CommitAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    Task<IUnitOfWorkWithRepositories> ConnectAndBeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken);
}