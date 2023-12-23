using System.Data;
using TransactionService.Application.Exceptions;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common;
using TransactionService.Common.Factories;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<string>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public CreateTransactionCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOdWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            var transaction = new Transaction(
                request.TransactionToken,
                request.FromAccountId,
                request.FromAccountType,
                request.FromUserId,
                request.FromCurrencyCode,
                request.FromAmount,
                request.ToAccountId,
                request.ToAccountType,
                request.ToUserId,
                request.ToCurrencyCode,
                request.ToAmount,
                DateTime.UtcNow,
                DateTime.UtcNow);

            string token = await unitOdWork.TransactionRepository.AddAsync(transaction).ConfigureAwait(false);

            await unitOdWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(token)
                .ConfigureAwait(false);
        }
        catch (DatabaseException)
        {
            await unitOdWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<string>("Unexpected database exception")
                .ConfigureAwait(false);
        }
    }
}