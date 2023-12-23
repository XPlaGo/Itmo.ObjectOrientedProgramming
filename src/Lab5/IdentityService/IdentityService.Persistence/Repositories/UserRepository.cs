using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.Contexts;
using IdentityService.Persistence.Mapping;
using Npgsql;

namespace IdentityService.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext, IEntityReader<User> entityReader, IEntityReader<long> idReader)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        EntityReader = entityReader;
        IdReader = idReader;
    }

    public ApplicationDbContext DbContext { get; }
    public IEntityReader<User> EntityReader { get; }
    public IEntityReader<long> IdReader { get; }

    public async Task<User?> GetByIdAsync(long id)
    {
        using var command = new NpgsqlCommand(
            "select id, username, password, salt, isblocked, role, created_date, updated_date from users where id = @id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("id", id);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        using var command = new NpgsqlCommand(
            "select id, username, password, salt, isblocked, role, created_date, updated_date from users where username = @username",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("username", username);
        return await ExecuteQueryableReaderAsync(command, EntityReader).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<List<User>> GetAllAsync()
    {
        using var command = new NpgsqlCommand(
            "select id, username, password, salt, isblocked, role, created_date, updated_date from users",
            DbContext.Connection,
            DbContext.Transaction);
        return await ExecuteQueryableReaderAsync(command, EntityReader).ToListAsync().ConfigureAwait(false);
    }

    public async Task<long> AddAsync(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        using var command = new NpgsqlCommand(
            "insert into users (username, password, salt, isblocked, role, created_date, updated_date)" +
            "values (@username, @password, @salt, @isBlocked, @roles, @created_date, @updated_date)" +
            "returning id",
            DbContext.Connection,
            DbContext.Transaction);
        command.Parameters.AddWithValue("username", entity.Username);
        command.Parameters.AddWithValue("password", entity.Password);
        command.Parameters.AddWithValue("salt", entity.Salt);
        command.Parameters.AddWithValue("isBlocked", entity.IsBlocked);
        command.Parameters.AddWithValue("roles", entity.Role);
        command.Parameters.AddWithValue("created_date", entity.CreatedDate);
        command.Parameters.AddWithValue("updated_date", entity.UpdatedDate);
        return await ExecuteQueryableReaderAsync(command, IdReader).FirstAsync().ConfigureAwait(false);
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    private static async Task<long> ExecuteNonQueryAsync(NpgsqlCommand command)
    {
        return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    private static async Task<long> ExecuteScalarAsync(NpgsqlCommand command)
    {
        return (long)(await command.ExecuteScalarAsync().ConfigureAwait(false) ??
                      throw new InvalidOperationException());
    }

    private static async IAsyncEnumerable<TEntity> ExecuteQueryableReaderAsync<TEntity>(
        NpgsqlCommand command,
        IEntityReader<TEntity> entityReader)
    {
        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
        {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                yield return entityReader.Read(reader);
            }
        }
    }
}