using System;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Validators;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class PcAssembly
{
    public PcAssembly(
        BiosDocument bios,
        CpuDocument cpu,
        HddDocument? hdd,
        MotherboardDocument motherboard,
        PcCaseDocument pcCase,
        PowerUnitDocument powerUnit,
        ProcessorCoolingSystemDocument processorCoolingSystem,
        RamDocument ram,
        SsdDocument? ssd,
        VideoCardDocument videoCard,
        XmpDocument xmp,
        WifiAdapterDocument? wifiAdapter)
    {
        PcAssemblyValidator.Validate(
            bios,
            cpu,
            hdd,
            motherboard,
            pcCase,
            powerUnit,
            processorCoolingSystem,
            ram,
            ssd,
            videoCard,
            xmp,
            wifiAdapter);

        Bios = bios;
        Cpu = cpu;
        Hdd = hdd;
        Motherboard = motherboard;
        PcCase = pcCase;
        PowerUnit = powerUnit;
        ProcessorCoolingSystem = processorCoolingSystem;
        Ram = ram;
        Ssd = ssd;
        VideoCard = videoCard;
        Xmp = xmp;
        WifiAdapter = wifiAdapter;
    }

    public BiosDocument Bios { get; private set; }
    public CpuDocument Cpu { get; private set; }
    public HddDocument? Hdd { get; private set; }
    public MotherboardDocument Motherboard { get; private set; }
    public PcCaseDocument PcCase { get; private set; }
    public PowerUnitDocument PowerUnit { get; private set; }
    public ProcessorCoolingSystemDocument ProcessorCoolingSystem { get; private set; }
    public RamDocument Ram { get; private set; }
    public SsdDocument? Ssd { get; private set; }
    public VideoCardDocument VideoCard { get; private set; }
    public XmpDocument Xmp { get; private set; }
    public WifiAdapterDocument? WifiAdapter { get; private set; }

    public static PcAssembly CopyWith(
        PcAssembly other,
        BiosDocument? bios = null,
        CpuDocument? cpu = null,
        HddDocument? hdd = null,
        MotherboardDocument? motherboard = null,
        PcCaseDocument? pcCase = null,
        PowerUnitDocument? powerUnit = null,
        ProcessorCoolingSystemDocument? processorCoolingSystem = null,
        RamDocument? ram = null,
        SsdDocument? ssd = null,
        VideoCardDocument? videoCard = null,
        XmpDocument? xmp = null,
        WifiAdapterDocument? wifiAdapter = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new PcAssembly(
            bios ?? other.Bios,
            cpu ?? other.Cpu,
            hdd ?? other.Hdd,
            motherboard ?? other.Motherboard,
            pcCase ?? other.PcCase,
            powerUnit ?? other.PowerUnit,
            processorCoolingSystem ?? other.ProcessorCoolingSystem,
            ram ?? other.Ram,
            ssd ?? other.Ssd,
            videoCard ?? other.VideoCard,
            xmp ?? other.Xmp,
            wifiAdapter ?? other.WifiAdapter);
    }
}