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

    public DocumentId Id { get; set; }
    public string ConnectionType { get; set; }
    public int Volume { get; set; }
    public float MaxWorkSpeed { get; set; }
    public float PowerConsumption { get; set; }
}