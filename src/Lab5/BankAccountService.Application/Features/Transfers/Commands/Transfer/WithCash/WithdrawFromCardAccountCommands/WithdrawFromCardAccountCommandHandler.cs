using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Common;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash.WithdrawFromCardAccountCommands;

public class WithdrawFromCardAccountCommandHandler : WithCashCardAccountCommandHandler
{
    public WithdrawFromCardAccountCommandHandler(
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ITransactionService transactionService)
        : base(unitOfWorkWithRepositoriesFactory, transactionService) { }

    protected override async Task Transfer(
        IUnitOfWorkWithRepositories unitOfWork,
        CardAccount cardAccount,
        decimal amount)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        ArgumentNullException.ThrowIfNull(cardAccount);

        if (cardAccount.Amount < amount) throw new NotEnoughMoneyException();

        cardAccount.Amount -= amount;
        cardAccount.UpdatedDate = DateTime.UtcNow;

        await unitOfWork.CardAccountRepository.UpdateAsync(cardAccount).ConfigureAwait(false);
    }

    protected override async Task CreateTransaction(
        ITransactionService transactionService,
        CardAccount cardAccount,
        WithCashCardAccountCommand request,
        Guid transactionToken)
    {
        ArgumentNullException.ThrowIfNull(transactionService);
        ArgumentNullException.ThrowIfNull(cardAccount);
        ArgumentNullException.ThrowIfNull(request);

        Result<string> transactionResult = await transactionService.Create(new CreateTransactionRequest(
                transactionToken.ToString(),
                request.AccountId,
                nameof(CardAccount),
                cardAccount.UserId,
                cardAccount.CurrencyCode,
                request.Amount,
                0,
                "Cash",
                cardAccount.UserId,
                cardAccount.CurrencyCode,
                request.Amount))
            .ConfigureAwait(false);

        if (transactionResult.Succeeded is false || transactionResult.Data is null)
            throw new ServiceException(transactionResult.Messages);
    }
}