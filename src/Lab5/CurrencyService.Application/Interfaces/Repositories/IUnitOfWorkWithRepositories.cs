using System.Data;

namespace CurrencyService.Application.Interfaces.Repositories;

public interface IUnitOfWorkWithRepositories : IDisposable
{
    ICurrencyRepository CurrencyRepository { get; }
    ICurrencyExchangeRepository CurrencyExchangeRepository { get; }

    ValueTask CommitAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    Task<IUnitOfWorkWithRepositories> ConnectAndBeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken);
}