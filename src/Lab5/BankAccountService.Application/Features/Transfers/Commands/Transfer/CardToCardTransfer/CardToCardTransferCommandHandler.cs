using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;

public class CardToCardTransferCommandHandler : IRequestHandler<TransferCommand, Result<TransferResponse>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICurrencyConversionService _conversionService;
    private readonly ITransactionService _transactionService;

    public CardToCardTransferCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICurrencyConversionService conversionService,
        ITransactionService transactionService)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _conversionService = conversionService;
        _transactionService = transactionService;
    }

    public async Task<Result<TransferResponse>> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateRequest(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        var transactionToken = Guid.NewGuid();

        try
        {
            AccountsContext accounts = await GetAccounts(unitOfWork, request).ConfigureAwait(false);

            CurrencyConversionResponse amounts = await Convert(accounts, request).ConfigureAwait(false);

            await Transfer(unitOfWork, accounts, amounts).ConfigureAwait(false);

            await CreateTransaction(accounts, amounts, transactionToken).ConfigureAwait(false);

            await unitOfWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .SuccessAsync(new TransferResponse(
                    amounts.FromAmount,
                    amounts.ToAmount))
                .ConfigureAwait(false);
        }
        catch (TransferRequestValidationException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (AccountNotFoundException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (NotEnoughMoneyException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (ServiceException exception)
        {
            await RollbackAsync(unitOfWork, transactionToken, cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Messages)
                .ConfigureAwait(false);
        }
    }

    private static void ValidateRequest(TransferCommand request)
    {
        if (request.Amount <= 0)
            throw new TransferRequestValidationException("Amount must have positive value");
    }

    private static async Task<AccountsContext> GetAccounts(
        IUnitOfWorkWithRepositories unitOfWork,
        TransferCommand request)
    {
        CardAccount fromAccount = await unitOfWork
                                      .CardAccountRepository
                                      .GetByIdAndUserIdAsync(
                                          request.FromAccountId,
                                          request.FromUserId)
                                      .ConfigureAwait(false) ??
                                  throw new AccountNotFoundException(
                                      "CardAccount",
                                      request.FromAccountId,
                                      request.FromUserId);

        CardAccount toAccount = await unitOfWork
                                    .CardAccountRepository
                                    .GetByIdAndUserIdAsync(
                                        request.ToAccountId,
                                        request.ToUserId)
                                    .ConfigureAwait(false) ??
                                throw new AccountNotFoundException(
                                    "CardAccount",
                                    request.ToAccountId,
                                    request.ToUserId);

        return new AccountsContext(fromAccount, toAccount);
    }

    private static async Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        AccountsContext accounts,
        CurrencyConversionResponse amounts)
    {
        if (accounts.FromAccount.Amount < amounts.FromAmount) throw new NotEnoughMoneyException();

        accounts.FromAccount.Amount -= amounts.FromAmount;
        accounts.FromAccount.UpdatedDate = DateTime.UtcNow;
        accounts.ToAccount.Amount += amounts.ToAmount;
        accounts.ToAccount.UpdatedDate = DateTime.UtcNow;

        await unitOfWork.CardAccountRepository.UpdateAsync(accounts.FromAccount).ConfigureAwait(false);
        await unitOfWork.CardAccountRepository.UpdateAsync(accounts.ToAccount).ConfigureAwait(false);
    }

    private async Task<CurrencyConversionResponse> Convert(AccountsContext accounts, TransferCommand request)
    {
        Result<CurrencyConversionResponse> amountsResult = await _conversionService.Convert(
                new CurrencyConversionRequest(
                    accounts.FromAccount.CurrencyCode,
                    accounts.ToAccount.CurrencyCode,
                    request.Amount))
            .ConfigureAwait(false);

        if (amountsResult.Succeeded is false || amountsResult.Data is null)
            throw new ServiceException(amountsResult.Messages);

        return amountsResult.Data;
    }

    private async Task CreateTransaction(
        AccountsContext accounts,
        CurrencyConversionResponse amounts,
        Guid transactionToken)
    {
        Result<string> transactionResult = await _transactionService.Create(new CreateTransactionRequest(
                transactionToken.ToString(),
                accounts.FromAccount.Id,
                "Card",
                accounts.FromAccount.UserId,
                accounts.FromAccount.CurrencyCode,
                amounts.FromAmount,
                accounts.ToAccount.Id,
                "Card",
                accounts.ToAccount.UserId,
                accounts.ToAccount.CurrencyCode,
                amounts.ToAmount))
            .ConfigureAwait(false);

        if (transactionResult.Succeeded is false || transactionResult.Data is null)
            throw new ServiceException(transactionResult.Messages);
    }

    private async Task RollbackAsync(
        IUnitOfWorkWithRepositories unitOfWork,
        Guid transactionToken,
        CancellationToken cancellationToken)
    {
        await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
        await RollbackTransactionService(transactionToken).ConfigureAwait(false);
    }

    private async Task RollbackTransactionService(Guid transactionToken)
    {
        await _transactionService
            .Delete(new DeleteTransactionRequest(transactionToken.ToString()))
            .ConfigureAwait(false);
    }

    private record AccountsContext(
        CardAccount FromAccount,
        CardAccount ToAccount);
}