using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public class PcAssemblyBuilder :
    IWithBios,
    IWithCpu,
    IWithMotherboard,
    IWithPowerUnit,
    IWithProcessorCoolingSystem,
    IWithVideoCard,
    IWithXmp,
    IWithRam,
    IWithSsdOrPcCase,
    IWithHddOrPcCase,
    IWithOrSsdOrHddOrPcCase,
    IWithWifiAdapterOrBuild
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
    private WifiAdapterDocument? WifiAdapter { get; set; }

    public IWithMotherboard Init()
    {
        return this;
    }

    public IWithCpu WithMotherboard(MotherboardDocument motherboardDocument)
    {
        Motherboard = motherboardDocument;
        return this;
    }

    public IWithProcessorCoolingSystem WithCpu(CpuDocument cpuDocument)
    {
        Cpu = cpuDocument;
        return this;
    }

    public IWithRam WithProcessorCoolingSystem(ProcessorCoolingSystemDocument processorCoolingSystemDocument)
    {
        ProcessorCoolingSystem = processorCoolingSystemDocument;
        return this;
    }

    public IWithBios WithRam(RamDocument ramDocument)
    {
        Ram = ramDocument;
        return this;
    }

    public IWithXmp WithBios(BiosDocument biosDocument)
    {
        Bios = biosDocument;
        return this;
    }

    public IWithVideoCard WithXmp(XmpDocument xmpDocument)
    {
        Xmp = xmpDocument;
        return this;
    }

    public IWithOrSsdOrHddOrPcCase WithVideoCard(VideoCardDocument videoCardDocument)
    {
        VideoCard = videoCardDocument;
        return this;
    }

    public IWithHddOrPcCase WithSsd(SsdDocument ssdDocument)
    {
        Ssd = ssdDocument;
        return this;
    }

    public IWithSsdOrPcCase WithHdd(HddDocument hddDocument)
    {
        Hdd = hddDocument;
        return this;
    }

    public IWithPowerUnit WithPcCase(PcCaseDocument pcCaseDocument)
    {
        PcCase = pcCaseDocument;
        return this;
    }

    public IWithWifiAdapterOrBuild WithPowerUnit(PowerUnitDocument powerUnitDocument)
    {
        PowerUnit = powerUnitDocument;
        return this;
    }

    public IBuild WithWifiAdapter(WifiAdapterDocument wifiAdapter)
    {
        WifiAdapter = wifiAdapter;
        return this;
    }

    public PcAssembly Build()
    {
        if (Bios is null) throw new BuilderNullComponentException(nameof(Bios));
        if (Cpu is null) throw new BuilderNullComponentException(nameof(Cpu));
        if (Motherboard is null) throw new BuilderNullComponentException(nameof(Motherboard));
        if (PcCase is null) throw new BuilderNullComponentException(nameof(PcCase));
        if (PowerUnit is null) throw new BuilderNullComponentException(nameof(PowerUnit));
        if (ProcessorCoolingSystem is null) throw new BuilderNullComponentException(nameof(ProcessorCoolingSystem));
        if (Ram is null) throw new BuilderNullComponentException(nameof(Ram));
        if (VideoCard is null) throw new BuilderNullComponentException(nameof(VideoCard));
        if (Xmp is null) throw new BuilderNullComponentException(nameof(Xmp));

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
            Xmp,
            WifiAdapter);
    }
}