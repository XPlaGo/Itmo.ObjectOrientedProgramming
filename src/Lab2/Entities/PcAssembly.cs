using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

public class PcAssembly
{
    public PcAssembly(
        BiosDocument bios,
        CpuDocument cpu,
        HddDocument hdd,
        MotherboardDocument motherboard,
        PcCaseDocument pcCase,
        PowerUnitDocument powerUnit,
        ProcessorCoolingSystemDocument processorCoolingSystem,
        RamDocument ram,
        SsdDocument ssd,
        VideoCardDocument videoCard,
        XmpDocument xmp)
    {
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
    }

    public BiosDocument Bios { get; set; }
    public CpuDocument Cpu { get; set; }
    public HddDocument Hdd { get; set; }
    public MotherboardDocument Motherboard { get; set; }
    public PcCaseDocument PcCase { get; set; }
    public PowerUnitDocument PowerUnit { get; set; }
    public ProcessorCoolingSystemDocument ProcessorCoolingSystem { get; set; }
    public RamDocument Ram { get; set; }
    public SsdDocument Ssd { get; set; }
    public VideoCardDocument VideoCard { get; set; }
    public XmpDocument Xmp { get; set; }
}