using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transfer;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;

public class CardToSavingsTransferCommandHandler : TransferCommandHandler<CardAccount, SavingsAccount>
{
    public CardToSavingsTransferCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICurrencyConversionService conversionService,
        ITransactionService transactionService)
        : base(unitOfWorkWithRepositoriesFactory, conversionService, transactionService) { }

    protected override async Task<AccountsContext<CardAccount, SavingsAccount>> GetAccounts(
        IUnitOfWorkWithRepositories unitOfWork,
        TransferCommand request)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(request);

        CardAccount fromAccount = await unitOfWork
                                      .CardAccountRepository
                                      .GetByIdAndUserIdAsync(
                                          request.FromAccountId,
                                          request.FromUserId)
                                      .ConfigureAwait(false) ??
                                  throw new AccountNotFoundException(
                                      nameof(CardAccount),
                                      request.FromAccountId);

        SavingsAccount toAccount = await unitOfWork
                                    .SavingsAccountRepository
                                    .GetByIdAsync(
                                        request.ToAccountId)
                                    .ConfigureAwait(false) ??
                                throw new AccountNotFoundException(
                                    nameof(SavingsAccount),
                                    request.ToAccountId);

        return new AccountsContext<CardAccount, SavingsAccount>(fromAccount, toAccount);
    }

    protected override async Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        AccountsContext<CardAccount, SavingsAccount> accounts,
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
        await unitOfWork.SavingsAccountRepository.UpdateAsync(accounts.ToAccount).ConfigureAwait(false);
    }
}