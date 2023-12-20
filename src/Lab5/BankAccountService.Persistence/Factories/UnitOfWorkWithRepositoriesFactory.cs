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
    private readonly IDepositAccountRepository _depositAccountRepository;

    public UnitOfWorkWithRepositoriesFactory(
        ApplicationDbContext applicationDbContext,
        ICardAccountRepository cardAccountRepository,
        ISavingsAccountRepository savingsAccountRepository,
        IDepositAccountRepository depositAccountRepository)
    {
        _applicationDbContext = applicationDbContext;
        _cardAccountRepository = cardAccountRepository;
        _savingsAccountRepository = savingsAccountRepository;
        _depositAccountRepository = depositAccountRepository;
    }

    public async Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var unitOfWork = new UnitOfWorkWithRepositories(
            _applicationDbContext,
            _cardAccountRepository,
            _savingsAccountRepository,
            _depositAccountRepository);
        await unitOfWork.ConnectAndBeginTransaction(isolationLevel, cancellationToken).ConfigureAwait(false);
        return unitOfWork;
    }
}