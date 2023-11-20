using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class ProcessorCoolingSystemDocument : IDocument
{
    public ProcessorCoolingSystemDocument(DocumentId id, float height, float width, IReadOnlyList<string> supportedSockets, float tdp)
    {
        Id = id;
        Height = height;
        Width = width;
        SupportedSockets = supportedSockets;
        Tdp = tdp;
    }

    public DocumentId Id { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public IReadOnlyList<string> SupportedSockets { get; set; }
    public float Tdp { get; set; }
}