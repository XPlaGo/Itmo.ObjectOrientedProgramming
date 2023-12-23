using IdentityService.Domain.Common;

namespace CurrencyService.Domain.Entities;

public class CurrencyExchange : BaseMetaInfoEntity
{
    public CurrencyExchange(
        long currencyFrom,
        long currencyTo,
        decimal exchangeRate,
        DateTime createdDate,
        DateTime updatedDate)
    {
        CurrencyFrom = currencyFrom;
        CurrencyTo = currencyTo;
        ExchangeRate = exchangeRate;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    public long CurrencyFrom { get; set; }

    public long CurrencyTo { get; set; }

    public decimal ExchangeRate { get; set; }
}