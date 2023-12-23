using System;
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

    public DocumentId Id { get; init; }
    public string Timings { get; init; }
    public float Voltage { get; init; }
    public long Frequency { get; init; }

    public static XmpDocument CopyWith(
        XmpDocument other,
        DocumentId? id = null,
        string? timings = null,
        float? voltage = null,
        long? frequency = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new XmpDocument(
            id ?? other.Id,
            timings ?? other.Timings,
            voltage ?? other.Voltage,
            frequency ?? other.Frequency);
    }
}