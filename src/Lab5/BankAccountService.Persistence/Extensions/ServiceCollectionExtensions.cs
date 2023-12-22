using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Domain.Entities;
using BankAccountService.Persistence.Contexts;
using BankAccountService.Persistence.Factories;
using BankAccountService.Persistence.Mapping;
using BankAccountService.Persistence.Repositories;
using CurrencyService.Persistence.Mapping;
using CurrencyService.Persistence.Repositories;
using IdentityService.Persistence.Mapping;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountService.Persistence.Extensions;

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
            .AddTransient<IEntityReader<CardAccount>, CardAccountReader>()
            .AddTransient<IEntityReader<SavingsAccount>, SavingsAccountReader>()
            .AddTransient<IEntityReader<long>, IdReader>()
            .AddScoped(typeof(IUnitOfWorkWithRepositories), typeof(UnitOfWorkWithRepositories))
            .AddScoped<ICardAccountRepository, CardAccountRepository>()
            .AddScoped<ISavingsAccountRepository, SavingsAccountRepository>();
    }
}