using System.Data;
using CurrencyService.Application.Interfaces.Repositories;

namespace CurrencyService.Application.Interfaces.Factories;

public interface IUnitOfWorkWithRepositoriesFactory
{
     Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken);
}