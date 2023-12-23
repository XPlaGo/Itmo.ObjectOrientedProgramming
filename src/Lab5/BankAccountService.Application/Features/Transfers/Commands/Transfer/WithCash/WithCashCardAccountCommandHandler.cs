using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash;

public abstract class WithCashCardAccountCommandHandler : IRequestHandler<WithCashCardAccountCommand, Result<WithCashCardAccountResponse>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ITransactionService _transactionService;

    protected WithCashCardAccountCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory, ITransactionService transactionService)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _transactionService = transactionService;
    }

    public async Task<Result<WithCashCardAccountResponse>> Handle(WithCashCardAccountCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        var transactionToken = Guid.NewGuid();

        try
        {
            ValidateRequest(request);

            CardAccount account = await GetAccount(unitOfWork, request).ConfigureAwait(false);

            await Transfer(unitOfWork, account, request.Amount).ConfigureAwait(false);

            await CreateTransaction(_transactionService, account, request, transactionToken).ConfigureAwait(false);

            await unitOfWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .SuccessAsync(new WithCashCardAccountResponse(
                    request.Amount,
                    account.Amount))
                .ConfigureAwait(false);
        }
        catch (DatabaseException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<WithCashCardAccountResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (TransferRequestValidationException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<WithCashCardAccountResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (AccountNotFoundException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<WithCashCardAccountResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (NotEnoughMoneyException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<WithCashCardAccountResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (ServiceException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<WithCashCardAccountResponse>(exception.Messages)
                .ConfigureAwait(false);
        }
    }

    protected abstract Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        CardAccount cardAccount,
        decimal amount);

    protected abstract Task CreateTransaction(
        ITransactionService transactionService,
        CardAccount cardAccount,
        WithCashCardAccountCommand request,
        Guid transactionToken);

    private static void ValidateRequest(WithCashCardAccountCommand request)
    {
        if (request.Amount <= 0)
            throw new TransferRequestValidationException("Amount must have positive value");
    }

    private async Task<CardAccount> GetAccount(
        IUnitOfWorkWithRepositories unitOfWork,
        WithCashCardAccountCommand request)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(request);

        return await unitOfWork
            .CardAccountRepository
            .GetByIdAndUserIdAsync(request.AccountId, request.UserId)
            .ConfigureAwait(false) ??
               throw new AccountNotFoundException(nameof(CardAccount), request.AccountId);
    }

    private async Task RollbackAsync(
        IUnitOfWorkWithRepositories unitOfWork,
        Guid transactionToken,
        CancellationToken cancellationToken)
    {
        if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
        await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
        await RollbackTransactionService(transactionToken).ConfigureAwait(false);
    }

    private async Task RollbackTransactionService(Guid transactionToken)
    {
        await _transactionService
            .Delete(new DeleteTransactionRequest(transactionToken.ToString()))
            .ConfigureAwait(false);
    }
}