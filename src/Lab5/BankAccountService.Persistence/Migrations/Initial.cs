using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace BankAccountService.Persistence.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        $"""
        create table card_accounts
        (
            id bigint primary key generated always as identity,
            amount decimal not null,
            user_id bigint not null,
            currency_code bigint not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );

        create table savings_accounts
        (
            id bigint primary key generated always as identity,
            amount decimal not null,
            user_id bigint not null,
            currency_code bigint not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );

        create table deposit_accounts
        (
            id bigint primary key generated always as identity,
            amount decimal not null,
            user_id bigint not null,
            currency_code bigint not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );

        insert into card_accounts (amount, user_id, currency_code, created_date, updated_date)
        values (100, 1, 1, now(), now());
        
        insert into card_accounts (amount, user_id, currency_code, created_date, updated_date)
        values (100, 2, 2, now(), now());
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table card_accounts;

        drop table savings_accounts;

        drop table deposit_accounts;
        """;
}