using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Dto;

public class MotherboardDto
{
    public DocumentId? Id { get; set; }
    public string? Socket { get; set; }
    public int PcieCount { get; set; }
    public int SataCount { get; set; }
    public IReadOnlyList<string>? SupportedMemoryVersions { get; set; }
    public bool XmpSupport { get; set; }
    public string? DdrStandard { get; set; }
    public int RamSlotsCount { get; set; }
    public string? FormFactor { get; set; }
    public DocumentRef<BiosDocument>? Bios { get; set; }

    public MotherboardDocument ToMotherboardDocument()
    {
        if (Id is null) throw new BuilderNullComponentException(nameof(Id));
        if (Socket is null) throw new BuilderNullComponentException(nameof(Socket));
        if (SupportedMemoryVersions is null) throw new BuilderNullComponentException(nameof(SupportedMemoryVersions));
        if (DdrStandard is null) throw new BuilderNullComponentException(nameof(DdrStandard));
        if (FormFactor is null) throw new BuilderNullComponentException(nameof(FormFactor));
        if (Bios is null) throw new BuilderNullComponentException(nameof(Bios));

        return new MotherboardDocument(
            Id,
            Socket,
            PcieCount,
            SataCount,
            SupportedMemoryVersions,
            XmpSupport,
            DdrStandard,
            RamSlotsCount,
            FormFactor,
            Bios);
    }
}