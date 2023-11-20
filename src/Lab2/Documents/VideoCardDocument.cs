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

    public DocumentId Id { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public long VideoMemory { get; set; }
    public string PcieVersion { get; set; }
    public long Frequency { get; set; }
    public float PowerConsumption { get; set; }
}