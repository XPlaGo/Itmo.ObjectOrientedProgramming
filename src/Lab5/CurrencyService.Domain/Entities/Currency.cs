using IdentityService.Domain.Common;

namespace CurrencyService.Domain.Entities;

public class Currency : BaseMetaInfoEntity
{
    public Currency(
        long currencyCode,
        string currencyName,
        DateTime createdDate,
        DateTime updatedDate)
    {
        CurrencyCode = currencyCode;
        CurrencyName = currencyName;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    public long CurrencyCode { get; set; }

    public string CurrencyName { get; set; }
}