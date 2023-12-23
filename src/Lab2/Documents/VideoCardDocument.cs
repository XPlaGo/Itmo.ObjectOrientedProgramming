using System;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class VideoCardDocument : IDocument
{
    public VideoCardDocument(DocumentId id, float height, float width, long videoMemory, string pcieVersion, long frequency, float powerConsumption)
    {
        Id = id;
        Height = height;
        Width = width;
        VideoMemory = videoMemory;
        PcieVersion = pcieVersion;
        Frequency = frequency;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; init; }
    public float Height { get; init; }
    public float Width { get; init; }
    public long VideoMemory { get; init; }
    public string PcieVersion { get; init; }
    public long Frequency { get; init; }
    public float PowerConsumption { get; init; }

    public static VideoCardDocument CopyWith(
        VideoCardDocument other,
        DocumentId? id = null,
        float? height = null,
        float? width = null,
        long? videoMemory = null,
        string? pcieVersion = null,
        long? frequency = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new VideoCardDocument(
            id ?? other.Id,
            height ?? other.Height,
            width ?? other.Width,
            videoMemory ?? other.VideoMemory,
            pcieVersion ?? other.PcieVersion,
            frequency ?? other.Frequency,
            powerConsumption ?? other.PowerConsumption);
    }
}