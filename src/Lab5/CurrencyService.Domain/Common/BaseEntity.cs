using IdentityService.Domain.Common.Interfaces;

namespace IdentityService.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public long Id { get; set; }
}