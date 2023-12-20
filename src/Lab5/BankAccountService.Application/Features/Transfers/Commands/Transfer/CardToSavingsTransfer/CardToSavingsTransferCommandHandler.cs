using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;

public class CardToSavingsTransferCommandHandler : IRequestHandler<TransferCommand, Result<TransferResponse>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICurrencyConversionService _conversionService;

    public CardToSavingsTransferCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICurrencyConversionService conversionService)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _conversionService = conversionService;
    }

    public async Task<Result<TransferResponse>> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Amount <= 0)
        {
            return await new ResultFactory()
                .FailureAsync<TransferResponse>("Amount must have positive value")
                .ConfigureAwait(false);
        }

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
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

            SavingsAccount toAccount = await unitOfWork
                                        .SavingsAccountRepository
                                        .GetByIdAndUserIdAsync(
                                            request.ToAccountId,
                                            request.ToUserId)
                                        .ConfigureAwait(false) ??
                                    throw new AccountNotFoundException(
                                        "DepositAccount",
                                        request.ToAccountId,
                                        request.ToUserId);

            Result<CurrencyConversionResponse> amountsResult = await _conversionService.Convert(
                    new CurrencyConversionRequest(
                        fromAccount.CurrencyCode,
                        toAccount.CurrencyCode,
                        request.Amount))
                .ConfigureAwait(false);

            if (amountsResult.Data is null)
            {
                return await new ResultFactory()
                    .FailureAsync<TransferResponse>(amountsResult.Messages)
                    .ConfigureAwait(false);
            }

            if (fromAccount.Amount < amountsResult.Data.FromAmount) throw new NotEnoughMoneyException();

            fromAccount.Amount -= amountsResult.Data.FromAmount;
            fromAccount.UpdatedDate = DateTime.UtcNow;

            toAccount.Amount += amountsResult.Data.ToAmount;
            toAccount.UpdatedDate = DateTime.UtcNow;

            await unitOfWork.CardAccountRepository.UpdateAsync(fromAccount).ConfigureAwait(false);
            await unitOfWork.SavingsAccountRepository.UpdateAsync(toAccount).ConfigureAwait(false);

            await unitOfWork.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(new TransferResponse(
                    amountsResult.Data.FromAmount,
                    amountsResult.Data.ToAmount))
                .ConfigureAwait(false);
        }
        catch (AccountNotFoundException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Message)
                .ConfigureAwait(false);
        }
        catch (NotEnoughMoneyException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .FailureAsync<TransferResponse>(exception.Message)
                .ConfigureAwait(false);
        }
    }
}