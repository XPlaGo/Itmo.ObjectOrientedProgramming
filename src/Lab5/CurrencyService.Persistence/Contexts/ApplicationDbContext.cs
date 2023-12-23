using System.Data;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Transactions;
using Npgsql;

namespace CurrencyService.Persistence.Contexts;

public class ApplicationDbContext : IDisposable
{
    public ApplicationDbContext(
        IPostgresConnectionProvider connectionProvider,
        IPostgresTransactionProvider transactionProvider)
    {
        ConnectionProvider = connectionProvider;
        TransactionProvider = transactionProvider;
    }

    public IPostgresConnectionProvider ConnectionProvider { get; }
    public IPostgresTransactionProvider TransactionProvider { get; }
    public NpgsqlTransaction? Transaction { get; private set;  }
    public NpgsqlConnection? Connection { get; private set; }

    public async Task<NpgsqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        NpgsqlConnection connection = await ConnectionProvider.GetConnectionAsync(cancellationToken).ConfigureAwait(false);
        Connection = connection;
        return connection;
    }

    public async Task<NpgsqlTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        if (Connection is null) throw new InvalidOperationException();
        Transaction = await Connection.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);

        return Transaction;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (Transaction is null) throw new InvalidOperationException();
        await Transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (Transaction is null) throw new InvalidOperationException();
        await Transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Transaction?.Dispose();
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}