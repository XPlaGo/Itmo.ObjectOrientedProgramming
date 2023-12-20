using System.Data;
using IdentityService.Application.Interfaces.Repositories;

namespace IdentityService.Application.Interfaces.Factories;

public interface IUnitOfWorkWithRepositoriesFactory
{
     Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken);
}