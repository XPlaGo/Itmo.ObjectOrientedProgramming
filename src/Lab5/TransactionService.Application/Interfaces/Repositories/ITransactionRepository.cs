using TransactionService.Domain.Entities;

namespace TransactionService.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    public Task<Transaction?> GetByTokenAsync(string token);

    public Task<IReadOnlyList<Transaction>> GetAllByUserId(long userId);

    public Task<string> AddAsync(Transaction entity);

    public Task<string?> UpdateAsync(Transaction entity);

    public Task<string?> DeleteAsync(string token);
}