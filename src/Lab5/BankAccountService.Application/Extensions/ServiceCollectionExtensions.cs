using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToDepositTranfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.CardToSavingsTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.DepositToCardTransfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.SavingsToCardTransfer;
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
        services.AddScoped<CardToDepositTransferCommandHandler>();
        services.AddScoped<DepositToCardTransferCommandHandler>();
        services.AddScoped<SavingsToCardTransferCommandHandler>();
    }
}