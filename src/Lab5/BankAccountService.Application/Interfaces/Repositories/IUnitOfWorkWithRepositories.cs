using System.Data;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface IUnitOfWorkWithRepositories : IDisposable
{
    ICardAccountRepository CardAccountRepository { get; }
    ISavingsAccountRepository SavingsAccountRepository { get; }
    IDepositAccountRepository DepositAccountRepository { get; }

    ValueTask CommitAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    Task<IUnitOfWorkWithRepositories> ConnectAndBeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken);
}