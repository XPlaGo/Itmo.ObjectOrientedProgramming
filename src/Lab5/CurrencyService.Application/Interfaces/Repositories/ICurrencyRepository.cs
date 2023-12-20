using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces.Repositories;

public interface ICurrencyRepository
{
    public Task<Currency?> GetByIdAsync(long currencyCode);

    public Task<List<Currency>> GetAllAsync();

    public Task<long> AddAsync(Currency entity);

    public Task UpdateAsync(Currency entity);

    public Task DeleteAsync(Currency entity);
}