using CurrencyService.Application.Features.CurrencyExchanges.Conversion;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddCommands();
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<ConversionCommandHandler>();
    }
}