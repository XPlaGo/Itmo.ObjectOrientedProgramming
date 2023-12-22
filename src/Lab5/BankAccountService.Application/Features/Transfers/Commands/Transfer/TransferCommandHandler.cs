using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Application.Models.Transfer;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer;

public abstract class TransferCommandHandler<TFromAccount, TToAccount> : IRequestHandler<TransferCommand, Result<TransferResponse>>
where TFromAccount : Account
where TToAccount : Account
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICurrencyConversionService _conversionService;
    private readonly ITransactionService _transactionService;

    protected TransferCommandHandler(
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
            AccountsContext<TFromAccount, TToAccount> accounts = await GetAccounts(unitOfWork, request).ConfigureAwait(false);

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

    protected abstract Task<AccountsContext<TFromAccount, TToAccount>> GetAccounts(
        IUnitOfWorkWithRepositories unitOfWork,
        TransferCommand request);

    protected abstract Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        AccountsContext<TFromAccount, TToAccount> accounts,
        CurrencyConversionResponse amounts);

    private static void ValidateRequest(TransferCommand request)
    {
        if (request.Amount <= 0)
            throw new TransferRequestValidationException("Amount must have positive value");
    }

    private async Task<CurrencyConversionResponse> Convert(AccountsContext<TFromAccount, TToAccount> accounts, TransferCommand request)
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
        AccountsContext<TFromAccount, TToAccount> accounts,
        CurrencyConversionResponse amounts,
        Guid transactionToken)
    {
        Result<string> transactionResult = await _transactionService.Create(new CreateTransactionRequest(
                transactionToken.ToString(),
                accounts.FromAccount.Id,
                typeof(TFromAccount).Name,
                accounts.FromAccount.UserId,
                accounts.FromAccount.CurrencyCode,
                amounts.FromAmount,
                accounts.ToAccount.Id,
                typeof(TToAccount).Name,
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
}