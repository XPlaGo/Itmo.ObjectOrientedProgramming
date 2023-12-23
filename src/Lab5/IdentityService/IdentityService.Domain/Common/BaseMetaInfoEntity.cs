using IdentityService.Domain.Common.Interfaces;

namespace IdentityService.Domain.Common;

public abstract class BaseMetaInfoEntity : BaseEntity, IMetaInfoEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}