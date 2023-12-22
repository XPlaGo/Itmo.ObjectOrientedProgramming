using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Infrastructure.Mapping;
using BankAccountService.Infrastructure.Services.CurrencyConversion;
using BankAccountService.Infrastructure.Services.JWT;
using BankAccountService.Infrastructure.Services.Transaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLevel(this IServiceCollection services, IConfiguration configuration, GrpcServicesSettings settings)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddMapping();
        services.AddServices(configuration, settings);
    }

    private static void AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TransactionMappingProfile));
        services.AddAutoMapper(typeof(CurrencyMappingProfile));
    }

    private static void AddServices(this IServiceCollection services, IConfiguration configuration, GrpcServicesSettings settings)
    {
        IConfigurationSection accessTokenConfiguration = configuration.GetSection("AccessTokenSettings");
        services.Configure<AccessTokenSettings>(accessTokenConfiguration);
        services.AddSingleton<IAuthTokenService, JwtService>();
        services.AddSingleton<GrpcServicesSettings>(_ => settings);

        services.AddSingleton<ICurrencyConversionService, CurrencyConversionService>();
        services.AddSingleton<ITransactionService, TransactionService>();
    }
}