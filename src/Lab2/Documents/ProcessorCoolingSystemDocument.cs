using System;
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

    public DocumentId Id { get; init; }
    public float Height { get; init; }
    public float Width { get; init; }
    public IReadOnlyList<string> SupportedSockets { get; init; }
    public float Tdp { get; init; }

    public static ProcessorCoolingSystemDocument CopyWith(
        ProcessorCoolingSystemDocument other,
        DocumentId? id = null,
        float? height = null,
        float? width = null,
        IReadOnlyList<string>? supportedSockets = null,
        float? tdp = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new ProcessorCoolingSystemDocument(
            id ?? other.Id,
            height ?? other.Height,
            width ?? other.Width,
            supportedSockets ?? other.SupportedSockets,
            tdp ?? other.Tdp);
    }
}