using BankAccountService.Domain.Common;

namespace BankAccountService.Domain.Entities;

public abstract class Account : BaseMetaInfoEntity
{
    public decimal Amount { get; set; }
    public long UserId { get; set; }
    public long CurrencyCode { get; set; }
}