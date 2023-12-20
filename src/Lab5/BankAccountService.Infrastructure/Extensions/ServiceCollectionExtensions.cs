using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Infrastructure.Services.CurrencyConversion;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLevel(this IServiceCollection services, string grpcHostAddress)
    {
        services.AddServices(grpcHostAddress);
    }

    private static void AddServices(this IServiceCollection services, string grpcHostAddress)
    {
        services.AddScoped<ICurrencyConversionService, CurrencyConversionService>(_ =>
            new CurrencyConversionService(grpcHostAddress));
    }
}