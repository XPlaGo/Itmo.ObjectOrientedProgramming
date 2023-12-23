using System.Data;

namespace IdentityService.Application.Interfaces.Repositories;

public interface IUnitOfWorkWithRepositories : IDisposable
{
    IUserRepository UserRepository { get; }

    ValueTask CommitAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    Task<IUnitOfWorkWithRepositories> ConnectAndBeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken);
}