using System.Data;
using BankAccountService.Application.Interfaces.Repositories;

namespace BankAccountService.Application.Interfaces.Factories;

public interface IUnitOfWorkWithRepositoriesFactory
{
     Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken);
}