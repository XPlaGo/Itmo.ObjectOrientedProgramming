using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transfer;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;

public class CardToCardTransferCommandHandler : TransferCommandHandler<CardAccount, CardAccount>
{
    public CardToCardTransferCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICurrencyConversionService conversionService,
        ITransactionService transactionService)
        : base(unitOfWorkWithRepositoriesFactory, conversionService, transactionService) { }

    protected override async Task<AccountsContext<CardAccount, CardAccount>> GetAccounts(
        IUnitOfWorkWithRepositories unitOfWork,
        TransferCommand request)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(request);

        if (request.FromAccountId == request.ToAccountId)
        {
            throw new TransferRequestValidationException(
                "You cannot transfer money from an account to the same account");
        }

        CardAccount fromAccount = await unitOfWork
                                      .CardAccountRepository
                                      .GetByIdAndUserIdAsync(
                                          request.FromAccountId,
                                          request.FromUserId)
                                      .ConfigureAwait(false) ??
                                  throw new AccountNotFoundException(
                                      nameof(CardAccount),
                                      request.FromAccountId);

        CardAccount toAccount = await unitOfWork
                                    .CardAccountRepository
                                    .GetByIdAsync(
                                        request.ToAccountId)
                                    .ConfigureAwait(false) ??
                                throw new AccountNotFoundException(
                                    nameof(CardAccount),
                                    request.ToAccountId);

        return new AccountsContext<CardAccount, CardAccount>(fromAccount, toAccount);
    }

    protected override async Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        AccountsContext<CardAccount, CardAccount> accounts,
        CurrencyConversionResponse amounts)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(accounts);
        ArgumentNullException.ThrowIfNull(amounts);

        if (accounts.FromAccount.Amount < amounts.FromAmount) throw new NotEnoughMoneyException();

        accounts.FromAccount.Amount -= amounts.FromAmount;
        accounts.FromAccount.UpdatedDate = DateTime.UtcNow;
        accounts.ToAccount.Amount += amounts.ToAmount;
        accounts.ToAccount.UpdatedDate = DateTime.UtcNow;

        await unitOfWork.CardAccountRepository.UpdateAsync(accounts.FromAccount).ConfigureAwait(false);
        await unitOfWork.CardAccountRepository.UpdateAsync(accounts.ToAccount).ConfigureAwait(false);
    }
}