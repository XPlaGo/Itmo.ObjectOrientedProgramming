using BankAccountService.Domain.Common;

namespace BankAccountService.Domain.Entities;

public class DepositAccount : BaseMetaInfoEntity
{
    public DepositAccount(
        long id,
        decimal amount,
        long userId,
        long currencyCode,
        DateTime createdDate,
        DateTime updatedDate)
    {
        Id = id;
        Amount = amount;
        UserId = userId;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        CurrencyCode = currencyCode;
    }

    public decimal Amount { get; set; }
    public long UserId { get; set; }
    public long CurrencyCode { get; set; }
}