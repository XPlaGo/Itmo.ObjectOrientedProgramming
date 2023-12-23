using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace CurrencyService.Persistence.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create table currencies
        (
            currency_code bigint primary key generated always as identity,
            currency_name text not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );

        create table currency_exchanges
        (
            currency_from bigint not null,
            currency_to bigint not null,
            exchange_rate decimal(18, 6),
            created_date timestamp not null,
            updated_date timestamp not null,
            primary key (currency_from, currency_to)
        );

        insert into currencies (currency_name, created_date, updated_date) 
        values ('rub', now(), now());
        
        insert into currencies (currency_name, created_date, updated_date) 
        values ('usd', now(), now());

        insert into currency_exchanges (currency_from, currency_to, exchange_rate, created_date, updated_date)
        values (1, 2, 0.01, now(), now());

        insert into currency_exchanges (currency_from, currency_to, exchange_rate, created_date, updated_date)
        values (2, 1, 100, now(), now());
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table currencies;

        drop table currency_exchanges;
        """;
}