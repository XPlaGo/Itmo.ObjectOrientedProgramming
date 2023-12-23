using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class CpuDocument : IDocument
{
    public CpuDocument(
        DocumentId id,
        long corsFrequency,
        int corsNumber,
        string socket,
        bool videoCorePresence,
        IReadOnlyList<string> supportedMemoryVersions,
        float tdp,
        float powerConsumption)
    {
        Id = id;
        CorsFrequency = corsFrequency;
        CorsNumber = corsNumber;
        Socket = socket;
        VideoCorePresence = videoCorePresence;
        SupportedMemoryVersions = supportedMemoryVersions;
        Tdp = tdp;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; init; }
    public long CorsFrequency { get; init; }
    public int CorsNumber { get; init; }
    public string Socket { get; init; }
    public bool VideoCorePresence { get; init; }
    public IReadOnlyList<string> SupportedMemoryVersions { get; init; }
    public float Tdp { get; init; }
    public float PowerConsumption { get; init; }

    public static CpuDocument CopyWith(
        CpuDocument other,
        DocumentId? id = null,
        long? corsFrequency = null,
        int? corsNumber = null,
        string? socket = null,
        bool? videoCorePresence = null,
        IReadOnlyList<string>? supportedMemoryVersions = null,
        float? tdp = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new CpuDocument(
            id ?? other.Id,
            corsFrequency ?? other.CorsFrequency,
            corsNumber ?? other.CorsNumber,
            socket ?? other.Socket,
            videoCorePresence ?? other.VideoCorePresence,
            supportedMemoryVersions ?? other.SupportedMemoryVersions,
            tdp ?? other.Tdp,
            powerConsumption ?? other.PowerConsumption);
    }
}