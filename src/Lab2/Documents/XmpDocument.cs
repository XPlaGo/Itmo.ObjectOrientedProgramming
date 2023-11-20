using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class XmpDocument : IDocument
{
    public XmpDocument(DocumentId id, string timings, float voltage, long frequency)
    {
        Id = id;
        Timings = timings;
        Voltage = voltage;
        Frequency = frequency;
    }

    public DocumentId Id { get; set; }
    public string Timings { get; set; }
    public float Voltage { get; set; }
    public long Frequency { get; set; }
}