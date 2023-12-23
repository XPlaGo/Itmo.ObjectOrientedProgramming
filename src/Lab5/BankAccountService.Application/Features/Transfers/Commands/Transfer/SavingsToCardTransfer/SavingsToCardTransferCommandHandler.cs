using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transfer;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;

public class SavingsToCardTransferCommandHandler : TransferCommandHandler<SavingsAccount, CardAccount>
{
    public SavingsToCardTransferCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICurrencyConversionService conversionService,
        ITransactionService transactionService)
        : base(unitOfWorkWithRepositoriesFactory, conversionService, transactionService) { }

    protected override async Task<AccountsContext<SavingsAccount, CardAccount>> GetAccounts(
        IUnitOfWorkWithRepositories unitOfWork,
        TransferCommand request)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(request);

        SavingsAccount fromAccount = await unitOfWork
                                      .SavingsAccountRepository
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
                                    nameof(SavingsAccount),
                                    request.ToAccountId);

        return new AccountsContext<SavingsAccount, CardAccount>(fromAccount, toAccount);
    }

    protected override async Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        AccountsContext<SavingsAccount, CardAccount> accounts,
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

        await unitOfWork.SavingsAccountRepository.UpdateAsync(accounts.FromAccount).ConfigureAwait(false);
        await unitOfWork.CardAccountRepository.UpdateAsync(accounts.ToAccount).ConfigureAwait(false);
    }
}