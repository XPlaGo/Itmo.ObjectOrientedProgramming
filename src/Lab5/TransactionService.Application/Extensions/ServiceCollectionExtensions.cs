using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Features.Transactions.Commands.CreateTransaction;
using TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;
using TransactionService.Application.Features.Transactions.Commands.UpdateTransaction;
using TransactionService.Application.Features.Transactions.Queries.GetAllByUserId;

namespace TransactionService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddCommands();
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<CreateTransactionCommandHandler>();
        services.AddScoped<DeleteTransactionCommandHandler>();
        services.AddScoped<UpdateTransactionCommandHandler>();
        services.AddScoped<GetAllByUserIdQueryHandler>();
    }
}