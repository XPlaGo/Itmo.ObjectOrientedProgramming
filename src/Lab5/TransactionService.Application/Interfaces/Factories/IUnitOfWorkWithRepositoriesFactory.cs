using System.Data;
using TransactionService.Application.Interfaces.Repositories;

namespace TransactionService.Application.Interfaces.Factories;

public interface IUnitOfWorkWithRepositoriesFactory
{
     Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken);
}