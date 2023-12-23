using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace IdentityService.Persistence.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type user_role as enum
        (
            'admin',
            'user'
        );
        
        create table users
        (
            id bigint primary key generated always as identity,
            username text not null,
            password text not null,
            salt text not null,
            isblocked boolean not null,
            role user_role not null,
            created_date timestamp not null,
            updated_date timestamp not null
        );
        
        insert into users (username, password, salt, isblocked, role, created_date, updated_date)
        values (
                'xplago',
                'ROh1BnbQLJqIDiP9WcR6ZnC8tmIBTfx3I4Ho0Dgzm3lxbMapEWnecTOZwR9wh7WPuEWZz8EBO9x7qvpy8zNkuQ==',
                '+3bnSZT+FLd7TNYG8lH4Rv3ErgDZfbNLwjIjm4D2PHzzHa2Fd2xtGPsSp8kkKKw7d11wQR0Q3JWTriCGIAwMHw==',
                false,
                'admin',
                now(),
                now()
        )
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;

        drop type user_role;
        """;
}