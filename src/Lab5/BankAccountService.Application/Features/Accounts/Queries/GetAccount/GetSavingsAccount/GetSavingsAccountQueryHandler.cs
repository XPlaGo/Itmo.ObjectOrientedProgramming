using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetSavingsAccount;

public class GetSavingsAccountQueryHandler : IRequestHandler<GetAccountRequest, Result<GetAccountResponse>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public GetSavingsAccountQueryHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<GetAccountResponse>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            SavingsAccount account = await unitOfWork
                .SavingsAccountRepository
                .GetByIdAndUserIdAsync(request.AccountId, request.UserId)
                .ConfigureAwait(false) ?? throw new AccountNotFoundException(nameof(CardAccount), request.AccountId);

            await unitOfWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(new GetAccountResponse(
                    account.Amount,
                    account.CurrencyCode))
                .ConfigureAwait(false);
        }
        catch (AccountNotFoundException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<GetAccountResponse>(
                    exception.Message)
                .ConfigureAwait(false);
        }
        catch (DatabaseException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<GetAccountResponse>(
                    exception.Message)
                .ConfigureAwait(false);
        }
    }
}