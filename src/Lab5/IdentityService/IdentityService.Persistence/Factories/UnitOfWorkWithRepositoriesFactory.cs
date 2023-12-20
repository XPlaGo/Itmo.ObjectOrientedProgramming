using System.Data;
using IdentityService.Application.Interfaces.Factories;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Persistence.Contexts;
using IdentityService.Persistence.Repositories;

namespace IdentityService.Persistence.Factories;

public class UnitOfWorkWithRepositoriesFactory : IUnitOfWorkWithRepositoriesFactory
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IUserRepository _userRepository;

    public UnitOfWorkWithRepositoriesFactory(ApplicationDbContext applicationDbContext, IUserRepository userRepository)
    {
        _applicationDbContext = applicationDbContext;
        _userRepository = userRepository;
    }

    public async Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var unit = new UnitOfWorkWithRepositories(_applicationDbContext, _userRepository);
        await unit.ConnectAndBeginTransaction(isolationLevel, cancellationToken).ConfigureAwait(false);
        return unit;
    }
}