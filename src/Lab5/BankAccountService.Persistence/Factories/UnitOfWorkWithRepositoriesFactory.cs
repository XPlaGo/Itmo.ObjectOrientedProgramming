using System.Data;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Persistence.Contexts;
using BankAccountService.Persistence.Repositories;

namespace BankAccountService.Persistence.Factories;

public class UnitOfWorkWithRepositoriesFactory : IUnitOfWorkWithRepositoriesFactory
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ICardAccountRepository _cardAccountRepository;
    private readonly ISavingsAccountRepository _savingsAccountRepository;

    public UnitOfWorkWithRepositoriesFactory(
        ApplicationDbContext applicationDbContext,
        ICardAccountRepository cardAccountRepository,
        ISavingsAccountRepository savingsAccountRepository)
    {
        _applicationDbContext = applicationDbContext;
        _cardAccountRepository = cardAccountRepository;
        _savingsAccountRepository = savingsAccountRepository;
    }

    public async Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var unitOfWork = new UnitOfWorkWithRepositories(
            _applicationDbContext,
            _cardAccountRepository,
            _savingsAccountRepository);
        await unitOfWork.ConnectAndBeginTransaction(isolationLevel, cancellationToken).ConfigureAwait(false);
        return unitOfWork;
    }
}