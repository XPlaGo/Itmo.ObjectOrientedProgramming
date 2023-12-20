using CurrencyService.Application.Interfaces.Factories;
using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Domain.Entities;
using CurrencyService.Persistence.Contexts;
using CurrencyService.Persistence.Factories;
using CurrencyService.Persistence.Mapping;
using CurrencyService.Persistence.Repositories;
using IdentityService.Persistence.Mapping;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyService.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, Action<PostgresConnectionConfiguration> configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
    }

    private static void AddDbContext(this IServiceCollection services, Action<PostgresConnectionConfiguration> configuration)
    {
        services.AddPlatformPostgres(builder => builder.Configure(configuration));
        services.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped<ApplicationDbContext, ApplicationDbContext>(provider =>
        {
            IPostgresConnectionProvider connectionProvider = provider.GetService<IPostgresConnectionProvider>() ?? throw new InvalidOperationException();
            IPostgresTransactionProvider transactionProvider = provider.GetService<IPostgresTransactionProvider>() ?? throw new InvalidOperationException();
            return new ApplicationDbContext(connectionProvider, transactionProvider);
        });
        services.AddScoped<IUnitOfWorkWithRepositoriesFactory, UnitOfWorkWithRepositoriesFactory>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<IEntityReader<Currency>, CurrencyReader>()
            .AddTransient<IEntityReader<CurrencyExchange>, CurrencyExchangeReader>()
            .AddTransient<IEntityReader<long>, IdReader>()
            .AddScoped(typeof(IUnitOfWorkWithRepositories), typeof(UnitOfWorkWithRepositories))
            .AddScoped<ICurrencyRepository, CurrencyRepository>()
            .AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRepository>();
    }
}