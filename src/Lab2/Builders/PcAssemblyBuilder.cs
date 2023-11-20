using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Validators;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class PcAssemblyBuilder
{
    private BiosDocument? Bios { get; set; }
    private CpuDocument? Cpu { get; set; }
    private HddDocument? Hdd { get; set; }
    private MotherboardDocument? Motherboard { get; set; }
    private PcCaseDocument? PcCase { get; set; }
    private PowerUnitDocument? PowerUnit { get; set; }
    private ProcessorCoolingSystemDocument? ProcessorCoolingSystem { get; set; }
    private RamDocument? Ram { get; set; }
    private SsdDocument? Ssd { get; set; }
    private VideoCardDocument? VideoCard { get; set; }
    private XmpDocument? Xmp { get; set; }

    public PcAssemblyBuilder WithBios(BiosDocument biosDocument)
    {
        Bios = biosDocument;
        return this;
    }

    public PcAssemblyBuilder WithCpu(CpuDocument cpuDocument)
    {
        Cpu = cpuDocument;
        return this;
    }

    public PcAssemblyBuilder WithHdd(HddDocument hddDocument)
    {
        Hdd = hddDocument;
        return this;
    }

    public PcAssemblyBuilder WithMotherboard(MotherboardDocument motherboardDocument)
    {
        Motherboard = motherboardDocument;
        return this;
    }

    public PcAssemblyBuilder WithPcCase(PcCaseDocument pcCaseDocument)
    {
        PcCase = pcCaseDocument;
        return this;
    }

    public PcAssemblyBuilder WithPowerUnit(PowerUnitDocument powerUnitDocument)
    {
        PowerUnit = powerUnitDocument;
        return this;
    }

    public PcAssemblyBuilder WithProcessorCoolingSystem(ProcessorCoolingSystemDocument processorCoolingSystemDocument)
    {
        ProcessorCoolingSystem = processorCoolingSystemDocument;
        return this;
    }

    public PcAssemblyBuilder WithSsd(SsdDocument ssdDocument)
    {
        Ssd = ssdDocument;
        return this;
    }

    public PcAssemblyBuilder WithVideoCard(VideoCardDocument videoCardDocument)
    {
        VideoCard = videoCardDocument;
        return this;
    }

    public PcAssemblyBuilder WithXmp(XmpDocument xmpDocument)
    {
        Xmp = xmpDocument;
        return this;
    }

    public PcAssembly Build()
    {
        if (Bios is null) throw new BuilderNullComponentException(nameof(Bios));
        if (Cpu is null) throw new BuilderNullComponentException(nameof(Cpu));
        if (Hdd is null) throw new BuilderNullComponentException(nameof(Hdd));
        if (Motherboard is null) throw new BuilderNullComponentException(nameof(Motherboard));
        if (PcCase is null) throw new BuilderNullComponentException(nameof(PcCase));
        if (PowerUnit is null) throw new BuilderNullComponentException(nameof(PowerUnit));
        if (ProcessorCoolingSystem is null) throw new BuilderNullComponentException(nameof(ProcessorCoolingSystem));
        if (Ram is null) throw new BuilderNullComponentException(nameof(Ram));
        if (Ssd is null) throw new BuilderNullComponentException(nameof(Ssd));
        if (VideoCard is null) throw new BuilderNullComponentException(nameof(VideoCard));
        if (Xmp is null) throw new BuilderNullComponentException(nameof(Xmp));

        PcAssemblyValidator.Validate(
            Bios,
            Cpu,
            Hdd,
            Motherboard,
            PcCase,
            PowerUnit,
            ProcessorCoolingSystem,
            Ram,
            Ssd,
            VideoCard,
            Xmp);

        return new PcAssembly(
            Bios,
            Cpu,
            Hdd,
            Motherboard,
            PcCase,
            PowerUnit,
            ProcessorCoolingSystem,
            Ram,
            Ssd,
            VideoCard,
            Xmp);
    }
}