using BankAccountService.Domain.Common.Interfaces;

namespace BankAccountService.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public long Id { get; set; }
}