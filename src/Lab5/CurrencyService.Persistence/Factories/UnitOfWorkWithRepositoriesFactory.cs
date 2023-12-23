using System.Data;
using CurrencyService.Application.Interfaces.Factories;
using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Persistence.Contexts;
using CurrencyService.Persistence.Repositories;

namespace CurrencyService.Persistence.Factories;

public class UnitOfWorkWithRepositoriesFactory : IUnitOfWorkWithRepositoriesFactory
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

    public UnitOfWorkWithRepositoriesFactory(ApplicationDbContext applicationDbContext, ICurrencyRepository currencyRepository, ICurrencyExchangeRepository currencyExchangeRepository)
    {
        _applicationDbContext = applicationDbContext;
        _currencyRepository = currencyRepository;
        _currencyExchangeRepository = currencyExchangeRepository;
    }

    public async Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var unitOfWork = new UnitOfWorkWithRepositories(
            _applicationDbContext,
            _currencyRepository,
            _currencyExchangeRepository);
        await unitOfWork.ConnectAndBeginTransaction(isolationLevel, cancellationToken).ConfigureAwait(false);
        return unitOfWork;
    }
}