using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface IDepositAccountRepository
{
    public Task<DepositAccount?> GetByIdAsync(long id);
    public Task<DepositAccount?> GetByIdAndUserIdAsync(long id, long userId);

    public Task<List<DepositAccount>> GetAllAsync();

    public Task<long> AddAsync(DepositAccount entity);

    public Task UpdateAsync(DepositAccount entity);

    public Task DeleteAsync(DepositAccount entity);
}