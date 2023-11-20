using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class MotherboardDocument : IDocument
{
    public MotherboardDocument(DocumentId id, string socket, int pcieCount, int sataCount, IReadOnlyList<string> supportedMemoryVersions, bool xmpSupport, string ddrStandard, int ramSlotsCount, string formFactor, DocumentRef<BiosDocument> bios)
    {
        Id = id;
        Socket = socket;
        PcieCount = pcieCount;
        SataCount = sataCount;
        SupportedMemoryVersions = supportedMemoryVersions;
        XmpSupport = xmpSupport;
        DdrStandard = ddrStandard;
        RamSlotsCount = ramSlotsCount;
        FormFactor = formFactor;
        Bios = bios;
    }

    public DocumentId Id { get; set; }
    public string Socket { get; set; }
    public int PcieCount { get; set; }
    public int SataCount { get; set; }
    public IReadOnlyList<string> SupportedMemoryVersions { get; set; }
    public bool XmpSupport { get; set; }
    public string DdrStandard { get; set; }
    public int RamSlotsCount { get; set; }
    public string FormFactor { get; set; }
    public DocumentRef<BiosDocument> Bios { get; set; }
}