using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Transactions;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Interfaces.Factories;
using TransactionService.Application.Interfaces.Repositories;
using TransactionService.Domain.Entities;
using TransactionService.Persistence.Contexts;
using TransactionService.Persistence.Factories;
using TransactionService.Persistence.Mapping;
using TransactionService.Persistence.Repositories;

namespace TransactionService.Persistence.Extensions;

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
            .AddTransient<IEntityReader<Transaction>, TransactionReader>()
            .AddTransient<IEntityReader<string>, IdReader>()
            .AddScoped(typeof(IUnitOfWorkWithRepositories), typeof(UnitOfWorkWithRepositories))
            .AddScoped<ITransactionRepository, TransactionRepository>();
    }
}