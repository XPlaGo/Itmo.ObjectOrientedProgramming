using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Interfaces.Repositories;

public interface ICurrencyExchangeRepository
{
    public Task<CurrencyExchange?> GetByCodesAsync(long fromCode, long toCode);

    public Task<List<CurrencyExchange>> GetAllAsync();

    public Task<long> AddAsync(CurrencyExchange entity);

    public Task UpdateAsync(CurrencyExchange entity);

    public Task DeleteAsync(CurrencyExchange entity);
}