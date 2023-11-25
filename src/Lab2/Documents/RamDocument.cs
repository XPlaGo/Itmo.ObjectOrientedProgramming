using System;
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

    public DocumentId Id { get; init; }
    public int Volume { get; init; }
    public IReadOnlyList<string> SupportedJedecAndVoltageFrequencyPairs { get; init; }
    public IReadOnlyList<string> SupportedXmpProfiles { get; init; }
    public string FormFactor { get; init; }
    public string DdrVersion { get; init; }
    public float PowerConsumption { get; init; }

    public static RamDocument CopyWith(
        RamDocument other,
        DocumentId? id = null,
        int? volume = null,
        IReadOnlyList<string>? supportedJedecAndVoltageFrequencyPairs = null,
        IReadOnlyList<string>? supportedXmpProfiles = null,
        string? formFactor = null,
        string? ddrVersion = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new RamDocument(
            id ?? other.Id,
            volume ?? other.Volume,
            supportedJedecAndVoltageFrequencyPairs ?? other.SupportedJedecAndVoltageFrequencyPairs,
            supportedXmpProfiles ?? other.SupportedXmpProfiles,
            formFactor ?? other.FormFactor,
            ddrVersion ?? other.DdrVersion,
            powerConsumption ?? other.PowerConsumption);
    }
}