using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class PowerUnitDocument : IDocument
{
    public PowerUnitDocument(DocumentId id, float maxLoad)
    {
        Id = id;
        MaxLoad = maxLoad;
    }

    public DocumentId Id { get; set; }
    public float MaxLoad { get; set; }
}