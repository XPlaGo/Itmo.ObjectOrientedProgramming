using System.Data;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Persistence.Contexts;
using TransactionService.Persistence.Repositories;

namespace TransactionService.Persistence.Factories;

public class UnitOfWorkWithRepositoriesFactory : IUnitOfWorkWithRepositoriesFactory
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ITransactionRepository _transactionRepository;

    public UnitOfWorkWithRepositoriesFactory(ApplicationDbContext applicationDbContext, ITransactionRepository transactionRepository)
    {
        _applicationDbContext = applicationDbContext;
        _transactionRepository = transactionRepository;
    }

    public async Task<IUnitOfWorkWithRepositories> Create(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var unitOfWork = new UnitOfWorkWithRepositories(
            _applicationDbContext,
            _transactionRepository);
        await unitOfWork.ConnectAndBeginTransaction(isolationLevel, cancellationToken).ConfigureAwait(false);
        return unitOfWork;
    }
}