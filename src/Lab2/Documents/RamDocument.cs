using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class RamDocument : IDocument
{
    public RamDocument(DocumentId id, int volume, IReadOnlyList<string> supportedJedecAndVoltageFrequencyPairs, IReadOnlyList<string> supportedXmpProfiles, string formFactor, string ddrVersion, float powerConsumption)
    {
        Id = id;
        Volume = volume;
        SupportedJedecAndVoltageFrequencyPairs = supportedJedecAndVoltageFrequencyPairs;
        SupportedXmpProfiles = supportedXmpProfiles;
        FormFactor = formFactor;
        DdrVersion = ddrVersion;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; set; }
    public int Volume { get; set; }
    public IReadOnlyList<string> SupportedJedecAndVoltageFrequencyPairs { get; set; }
    public IReadOnlyList<string> SupportedXmpProfiles { get; set; }
    public string FormFactor { get; set; }
    public string DdrVersion { get; set; }
    public float PowerConsumption { get; set; }
}