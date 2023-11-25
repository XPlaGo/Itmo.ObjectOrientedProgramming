using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

public class DocumentId
{
    public DocumentId(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public override int GetHashCode()
    {
        return Id.GetHashCode(StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        if (obj is not DocumentId otherId) return false;
        return Id == otherId.Id;
    }
}