using System;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class SsdDocument : IDocument
{
    public SsdDocument(DocumentId id, string connectionType, int volume, float maxWorkSpeed, float powerConsumption)
    {
        Id = id;
        ConnectionType = connectionType;
        Volume = volume;
        MaxWorkSpeed = maxWorkSpeed;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; init; }
    public string ConnectionType { get; set; }
    public int Volume { get; set; }
    public float MaxWorkSpeed { get; set; }
    public float PowerConsumption { get; set; }

    public static SsdDocument CopyWith(
        SsdDocument other,
        DocumentId? id = null,
        string? connectionType = null,
        int? volume = null,
        float? maxWorkSpeed = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new SsdDocument(
            id ?? other.Id,
            connectionType ?? other.ConnectionType,
            volume ?? other.Volume,
            maxWorkSpeed ?? other.MaxWorkSpeed,
            powerConsumption ?? other.PowerConsumption);
    }
}