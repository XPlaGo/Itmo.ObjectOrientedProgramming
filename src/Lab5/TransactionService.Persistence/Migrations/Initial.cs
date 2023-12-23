using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace TransactionService.Persistence.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create table transactions
        (
            transaction_token text primary key,
            from_account_id bigint not null,
            from_account_type text not null,
            from_user_id bigint not null,
            from_currency_code bigint not null,
            from_amount decimal not null,
            to_account_id bigint not null,
            to_account_type text not null,
            to_user_id bigint not null,
            to_currency_code bigint not null,
            to_amount decimal not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table transactions;
        """;
}