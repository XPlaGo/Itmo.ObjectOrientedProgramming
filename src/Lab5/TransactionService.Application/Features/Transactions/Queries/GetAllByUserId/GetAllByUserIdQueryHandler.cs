using System.Data;
using TransactionService.Application.Exceptions;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Common;
using TransactionService.Common.Factories;

namespace TransactionService.Application.Features.Transactions.Queries.GetAllByUserId;

public class GetAllByUserIdQueryHandler : IRequestHandler<GetAllByUserIdRequest, Result<IReadOnlyList<GetAllByUserIdResponse>>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public GetAllByUserIdQueryHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<IReadOnlyList<GetAllByUserIdResponse>>> Handle(GetAllByUserIdRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOdWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            IReadOnlyList<GetAllByUserIdResponse> transactions = (await unitOdWork
                    .TransactionRepository
                    .GetAllByUserId(request.UserId)
                    .ConfigureAwait(false))
                .Select(transaction => new GetAllByUserIdResponse(
                    transaction.TransactionToken,
                    transaction.FromAccountId,
                    transaction.FromAccountType,
                    transaction.FromUserId,
                    transaction.FromCurrencyCode,
                    transaction.FromAmount,
                    transaction.ToAccountId,
                    transaction.ToAccountType,
                    transaction.ToUserId,
                    transaction.ToCurrencyCode,
                    transaction.ToAmount,
                    transaction.CreatedDate,
                    transaction.UpdatedDate))
                .ToList();

            await unitOdWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(transactions)
                .ConfigureAwait(false);
        }
        catch (DatabaseException)
        {
            await unitOdWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<IReadOnlyList<GetAllByUserIdResponse>>("Unexpected database exception")
                .ConfigureAwait(false);
        }
    }
}