using System.Data;
using TransactionService.Application.Exceptions;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common;
using TransactionService.Common.Factories;

namespace TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<string>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public DeleteTransactionCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<string>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            string token = await unitOfWork.TransactionRepository.DeleteAsync(request.TransactionToken)
                .ConfigureAwait(false) ??
                            throw new TransactionNotFoundException(request.TransactionToken);

            await unitOfWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory().SuccessAsync(token).ConfigureAwait(false);
        }
        catch (TransactionNotFoundException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<string>(exception.Message).ConfigureAwait(false);
        }
        catch (DatabaseException)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<string>("Unexpected database exception")
                .ConfigureAwait(false);
        }
    }
}