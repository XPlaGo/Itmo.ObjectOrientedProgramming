using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface ICardAccountRepository
{
    public Task<CardAccount?> GetByIdAsync(long id);
    public Task<CardAccount?> GetByIdAndUserIdAsync(long id, long userId);

    public Task<List<CardAccount>> GetAllAsync();

    public Task<long> AddAsync(CardAccount entity);

    public Task UpdateAsync(CardAccount entity);

    public Task DeleteAsync(CardAccount entity);
}