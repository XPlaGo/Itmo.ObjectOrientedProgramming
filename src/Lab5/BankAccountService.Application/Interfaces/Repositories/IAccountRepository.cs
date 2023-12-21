using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface IAccountRepository<TAccount>
    where TAccount : Account
{
    public Task<TAccount?> GetByIdAsync(long id);
    public Task<TAccount?> GetByIdAndUserIdAsync(long id, long userId);

    public Task<List<TAccount>> GetAllAsync();

    public Task<long> AddAsync(TAccount entity);

    public Task UpdateAsync(TAccount entity);

    public Task DeleteAsync(TAccount entity);
}