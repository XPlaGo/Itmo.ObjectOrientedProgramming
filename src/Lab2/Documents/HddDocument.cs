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

    public DocumentId Id { get; set; }
    public int Volume { get; set; }
    public float MaxRotationSpeed { get; set; }
    public float PowerConsumption { get; set; }
}