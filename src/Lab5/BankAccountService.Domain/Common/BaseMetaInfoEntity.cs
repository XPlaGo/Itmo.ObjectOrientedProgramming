using BankAccountService.Domain.Common.Interfaces;

namespace BankAccountService.Domain.Common;

public abstract class BaseMetaInfoEntity : BaseEntity, IMetaInfoEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}