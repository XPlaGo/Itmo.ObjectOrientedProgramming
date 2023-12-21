using System.Data;
using TransactionService.Application.Exceptions;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common;
using TransactionService.Common.Factories;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Result<string>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public UpdateTransactionCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<string>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
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

            string token = await unitOdWork
                .TransactionRepository
                .UpdateAsync(transaction)
                .ConfigureAwait(false) ??
                           throw new TransactionNotFoundException(request.TransactionToken);

            await unitOdWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(token)
                .ConfigureAwait(false);
        }
        catch (TransactionNotFoundException exception)
        {
            await unitOdWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<string>(exception.Message).ConfigureAwait(false);
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