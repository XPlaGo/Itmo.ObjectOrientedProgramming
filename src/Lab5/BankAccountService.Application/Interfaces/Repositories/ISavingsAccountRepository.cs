using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface ISavingsAccountRepository
{
    public Task<SavingsAccount?> GetByIdAsync(long id);
    public Task<SavingsAccount?> GetByIdAndUserIdAsync(long id, long userId);

    public Task<List<SavingsAccount>> GetAllAsync();

    public Task<long> AddAsync(SavingsAccount entity);

    public Task UpdateAsync(SavingsAccount entity);

    public Task DeleteAsync(SavingsAccount entity);
}