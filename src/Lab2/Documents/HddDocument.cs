using System;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class HddDocument : IDocument
{
    public HddDocument(DocumentId id, int volume, float maxRotationSpeed, float powerConsumption)
    {
        Id = id;
        Volume = volume;
        MaxRotationSpeed = maxRotationSpeed;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; init; }
    public int Volume { get; init; }
    public float MaxRotationSpeed { get; init; }
    public float PowerConsumption { get; init; }

    public static HddDocument CopyWith(
        HddDocument other,
        DocumentId? id = null,
        int? volume = null,
        float? maxRotationSpeed = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new HddDocument(
            id ?? other.Id,
            volume ?? other.Volume,
            maxRotationSpeed ?? other.MaxRotationSpeed,
            powerConsumption ?? other.PowerConsumption);
    }
}