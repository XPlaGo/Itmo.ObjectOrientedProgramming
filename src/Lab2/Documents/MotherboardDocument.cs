using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentRefs;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class MotherboardDocument : IDocument
{
    public MotherboardDocument(
        DocumentId id,
        string socket,
        int pcieCount,
        int sataCount,
        IReadOnlyList<string> supportedMemoryVersions,
        bool xmpSupport,
        string ddrStandard,
        int ramSlotsCount,
        string formFactor,
        DocumentRef bios,
        bool hasBuiltinWifiAdapter)
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
        HasBuiltinWifiAdapter = hasBuiltinWifiAdapter;
    }

    public DocumentId Id { get; init; }
    public string Socket { get; init; }
    public int PcieCount { get; init; }
    public int SataCount { get; init; }
    public IReadOnlyList<string> SupportedMemoryVersions { get; init; }
    public bool XmpSupport { get; init; }
    public string DdrStandard { get; init; }
    public int RamSlotsCount { get; init; }
    public string FormFactor { get; init; }
    public DocumentRef Bios { get; init; }
    public bool HasBuiltinWifiAdapter { get; init; }

    public static MotherboardDocument CopyWith(
        MotherboardDocument other,
        DocumentId? id = null,
        string? socket = null,
        int? pcieCount = null,
        int? sataCount = null,
        IReadOnlyList<string>? supportedMemoryVersions = null,
        bool? xmpSupport = null,
        string? ddrStandard = null,
        int? ramSlotsCount = null,
        string? formFactor = null,
        DocumentRef? bios = null,
        bool? hasBuiltinWifiAdapter = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new MotherboardDocument(
            id ?? other.Id,
            socket ?? other.Socket,
            pcieCount ?? other.PcieCount,
            sataCount ?? other.SataCount,
            supportedMemoryVersions ?? other.SupportedMemoryVersions,
            xmpSupport ?? other.XmpSupport,
            ddrStandard ?? other.DdrStandard,
            ramSlotsCount ?? other.RamSlotsCount,
            formFactor ?? other.FormFactor,
            bios ?? other.Bios,
            hasBuiltinWifiAdapter ?? other.HasBuiltinWifiAdapter);
    }
}