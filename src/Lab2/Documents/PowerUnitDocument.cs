using System;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class PowerUnitDocument : IDocument
{
    public PowerUnitDocument(DocumentId id, float maxLoad)
    {
        Id = id;
        MaxLoad = maxLoad;
    }

    public DocumentId Id { get; init; }
    public float MaxLoad { get; init; }

    public static PowerUnitDocument CopyWith(
        PowerUnitDocument other,
        DocumentId? id = null,
        float? maxLoad = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new PowerUnitDocument(
            id ?? other.Id,
            maxLoad ?? other.MaxLoad);
    }
}