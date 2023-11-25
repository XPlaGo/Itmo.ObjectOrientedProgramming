using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class BiosDocument : IDocument
{
    public BiosDocument(
        DocumentId id,
        string type,
        string version,
        IReadOnlyList<DocumentRef> cpuSupportedList)
    {
        Id = id;
        Type = type;
        Version = version;
        CpuSupportedList = cpuSupportedList;
    }

    public DocumentId Id { get; init; }
    public string Type { get; init; }
    public string Version { get; init; }
    public IReadOnlyList<DocumentRef> CpuSupportedList { get; init; }

    public static BiosDocument CopyWith(
        BiosDocument other,
        DocumentId? id = null,
        string? type = null,
        string? version = null,
        IReadOnlyList<DocumentRef>? cpuSupportedList = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new BiosDocument(
            id ?? other.Id,
            type ?? other.Type,
            version ?? other.Version,
            cpuSupportedList ?? other.CpuSupportedList);
    }
}