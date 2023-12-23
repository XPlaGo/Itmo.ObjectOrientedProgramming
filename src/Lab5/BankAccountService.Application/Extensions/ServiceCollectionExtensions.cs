using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Card;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Savings;
using BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetCardAccount;
using BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetSavingsAccount;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash.TopUpCardAccountCommands;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash.WithdrawFromCardAccountCommands;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddCommands();
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<CardToCardTransferCommandHandler>();
        services.AddScoped<CardToSavingsTransferCommandHandler>();
        services.AddScoped<SavingsToCardTransferCommandHandler>();
        services.AddScoped<TopUpCardAccountCommandHandler>();
        services.AddScoped<WithdrawFromCardAccountCommandHandler>();
        services.AddScoped<CreateCardAccountCommandHandler>();
        services.AddScoped<CreateSavingsAccountCommandHandler>();
        services.AddScoped<GetCardAccountQueryHandler>();
        services.AddScoped<GetSavingsAccountQueryHandler>();
    }
}