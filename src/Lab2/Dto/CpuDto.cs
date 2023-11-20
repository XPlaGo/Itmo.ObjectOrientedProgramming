using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Dto;

public class CpuDto
{
    public DocumentId? Id { get; set; }
    public long CorsFrequency { get; set; }
    public int CorsNumber { get; set; }
    public string? Socket { get; set; }
    public bool VideoCorePresence { get; set; }
    public IReadOnlyList<string>? SupportedMemoryVersions { get; set; }
    public float Tdp { get; set; }
    public float PowerConsumption { get; set; }

    public CpuDocument ToCpuDocument()
    {
        if (Id is null) throw new BuilderNullComponentException(nameof(Id));
        if (SupportedMemoryVersions is null) throw new BuilderNullComponentException(nameof(SupportedMemoryVersions));
        if (Socket is null) throw new BuilderNullComponentException(nameof(Socket));

        return new CpuDocument(
            Id,
            CorsFrequency,
            CorsNumber,
            Socket,
            VideoCorePresence,
            SupportedMemoryVersions,
            Tdp,
            PowerConsumption);
    }
}