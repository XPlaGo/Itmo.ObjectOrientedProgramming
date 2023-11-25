using System.IO;
using Itmo.ObjectOrientedProgramming.Lab2.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Validator;
using Itmo.ObjectOrientedProgramming.Lab2.Factories.ComponentRepository;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class Test
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ComponentJsonRepositoryFactory _factory = new();
    private readonly Repository<BiosDocument> _biosRepository;
    private readonly Repository<CpuDocument> _cpuRepository;
    private readonly Repository<HddDocument> _hddRepository;
    private readonly Repository<MotherboardDocument> _motherboardRepository;
    private readonly Repository<PcCaseDocument> _pcCaseRepository;
    private readonly Repository<PowerUnitDocument> _powerUnitRepository;
    private readonly Repository<ProcessorCoolingSystemDocument> _coolingSystemRepository;
    private readonly Repository<RamDocument> _ramRepository;
    private readonly Repository<SsdDocument> _ssdRepository;
    private readonly Repository<VideoCardDocument> _videoCardRepository;
    private readonly Repository<WifiAdapterDocument> _wifiAdapterRepository;
    private readonly Repository<XmpDocument> _xmpRepository;

    public Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        _biosRepository = _factory.CreateJsonDocumentRepository<BiosDocument>(new FileInfo("Database/bios.json"));
        _cpuRepository = _factory.CreateJsonDocumentRepository<CpuDocument>(new FileInfo("Database/cpu.json"));
        _hddRepository = _factory.CreateJsonDocumentRepository<HddDocument>(new FileInfo("Database/hdd.json"));
        _motherboardRepository = _factory.CreateJsonDocumentRepository<MotherboardDocument>(new FileInfo("Database/motherboard.json"));
        _pcCaseRepository = _factory.CreateJsonDocumentRepository<PcCaseDocument>(new FileInfo("Database/pccase.json"));
        _powerUnitRepository = _factory.CreateJsonDocumentRepository<PowerUnitDocument>(new FileInfo("Database/powerunit.json"));
        _coolingSystemRepository = _factory.CreateJsonDocumentRepository<ProcessorCoolingSystemDocument>(new FileInfo("Database/processorcoolingsystem.json"));
        _ramRepository = _factory.CreateJsonDocumentRepository<RamDocument>(new FileInfo("Database/ram.json"));
        _ssdRepository = _factory.CreateJsonDocumentRepository<SsdDocument>(new FileInfo("Database/ssd.json"));
        _videoCardRepository = _factory.CreateJsonDocumentRepository<VideoCardDocument>(new FileInfo("Database/videocard.json"));
        _wifiAdapterRepository = _factory.CreateJsonDocumentRepository<WifiAdapterDocument>(new FileInfo("Database/wifiadapter.json"));
        _xmpRepository = _factory.CreateJsonDocumentRepository<XmpDocument>(new FileInfo("Database/xmp.json"));
    }

    [Fact]
    public void Valid()
    {
        BiosDocument bios = _biosRepository.GetById(new DocumentId("12346"));
        CpuDocument cpu = _cpuRepository.GetById(new DocumentId("67891"));
        HddDocument hdd = _hddRepository.GetById(new DocumentId("13579"));
        MotherboardDocument motherboard = _motherboardRepository.GetById(new DocumentId("24680"));
        PcCaseDocument pcCase = _pcCaseRepository.GetById(new DocumentId("11221"));
        PowerUnitDocument powerUnit = _powerUnitRepository.GetById(new DocumentId("33443"));
        ProcessorCoolingSystemDocument coolingSystem = _coolingSystemRepository.GetById(new DocumentId("55668"));
        RamDocument ram = _ramRepository.GetById(new DocumentId("77888"));
        SsdDocument ssd = _ssdRepository.GetById(new DocumentId("99001"));
        VideoCardDocument videoCard = _videoCardRepository.GetById(new DocumentId("11224"));
        WifiAdapterDocument wifiAdapter = _wifiAdapterRepository.GetById(new DocumentId("33446"));
        XmpDocument xmp = _xmpRepository.GetById(new DocumentId("54322"));

        new PcAssemblyBuilder().Init().WithMotherboard(motherboard).WithCpu(cpu)
            .WithProcessorCoolingSystem(coolingSystem).WithRam(ram).WithBios(bios).WithXmp(xmp).WithVideoCard(videoCard)
            .WithHdd(hdd).WithSsd(ssd).WithPcCase(pcCase).WithPowerUnit(powerUnit).WithWifiAdapter(wifiAdapter).Build();
    }

    [Fact]
    public void NonValid()
    {
        BiosDocument bios = _biosRepository.GetById(new DocumentId("12345"));
        CpuDocument cpu = _cpuRepository.GetById(new DocumentId("67891"));
        HddDocument hdd = _hddRepository.GetById(new DocumentId("13579"));
        MotherboardDocument motherboard = _motherboardRepository.GetById(new DocumentId("24680"));
        PcCaseDocument pcCase = _pcCaseRepository.GetById(new DocumentId("11221"));
        PowerUnitDocument powerUnit = _powerUnitRepository.GetById(new DocumentId("33443"));
        ProcessorCoolingSystemDocument coolingSystem = _coolingSystemRepository.GetById(new DocumentId("55668"));
        RamDocument ram = _ramRepository.GetById(new DocumentId("77888"));
        SsdDocument ssd = _ssdRepository.GetById(new DocumentId("99001"));
        VideoCardDocument videoCard = _videoCardRepository.GetById(new DocumentId("11224"));
        WifiAdapterDocument wifiAdapter = _wifiAdapterRepository.GetById(new DocumentId("33446"));
        XmpDocument xmp = _xmpRepository.GetById(new DocumentId("54322"));

        Assert.Throws<PcAssemblyValidatorException>(() =>
        {
            PcAssembly assembly = new PcAssemblyBuilder().Init().WithMotherboard(motherboard).WithCpu(cpu)
                .WithProcessorCoolingSystem(coolingSystem).WithRam(ram).WithBios(bios).WithXmp(xmp)
                .WithVideoCard(videoCard)
                .WithHdd(hdd).WithSsd(ssd).WithPcCase(pcCase).WithPowerUnit(powerUnit).WithWifiAdapter(wifiAdapter)
                .Build();
        });
    }

    [Fact]
    public void NonValidPowerUnit()
    {
        BiosDocument bios = _biosRepository.GetById(new DocumentId("12346"));
        CpuDocument cpu = _cpuRepository.GetById(new DocumentId("67891"));
        HddDocument hdd = _hddRepository.GetById(new DocumentId("13579"));
        MotherboardDocument motherboard = _motherboardRepository.GetById(new DocumentId("24680"));
        PcCaseDocument pcCase = _pcCaseRepository.GetById(new DocumentId("11221"));
        PowerUnitDocument powerUnit = _powerUnitRepository.GetById(new DocumentId("33443"));
        ProcessorCoolingSystemDocument coolingSystem = _coolingSystemRepository.GetById(new DocumentId("55667"));
        RamDocument ram = _ramRepository.GetById(new DocumentId("77888"));
        SsdDocument ssd = _ssdRepository.GetById(new DocumentId("99001"));
        VideoCardDocument videoCard = _videoCardRepository.GetById(new DocumentId("11224"));
        WifiAdapterDocument wifiAdapter = _wifiAdapterRepository.GetById(new DocumentId("33446"));
        XmpDocument xmp = _xmpRepository.GetById(new DocumentId("54322"));

        PcAssemblyValidatorException exception = Assert.Throws<PcAssemblyValidatorException>(() =>
        {
            PcAssembly assembly = new PcAssemblyBuilder().Init().WithMotherboard(motherboard).WithCpu(cpu)
                .WithProcessorCoolingSystem(coolingSystem).WithRam(ram).WithBios(bios).WithXmp(xmp)
                .WithVideoCard(videoCard)
                .WithHdd(hdd).WithSsd(ssd).WithPcCase(pcCase).WithPowerUnit(powerUnit).WithWifiAdapter(wifiAdapter)
                .Build();
        });
        Assert.Equal("CPU TDP is more than TDP of processor cooling system", exception.Message);
    }

    [Fact]
    public void NonValidInfo()
    {
        BiosDocument bios = _biosRepository.GetById(new DocumentId("12345"));
        CpuDocument cpu = _cpuRepository.GetById(new DocumentId("67891"));
        HddDocument hdd = _hddRepository.GetById(new DocumentId("13579"));
        MotherboardDocument motherboard = _motherboardRepository.GetById(new DocumentId("24680"));
        PcCaseDocument pcCase = _pcCaseRepository.GetById(new DocumentId("11221"));
        PowerUnitDocument powerUnit = _powerUnitRepository.GetById(new DocumentId("33443"));
        ProcessorCoolingSystemDocument coolingSystem = _coolingSystemRepository.GetById(new DocumentId("55668"));
        RamDocument ram = _ramRepository.GetById(new DocumentId("77888"));
        SsdDocument ssd = _ssdRepository.GetById(new DocumentId("99000"));
        VideoCardDocument videoCard = _videoCardRepository.GetById(new DocumentId("11224"));
        WifiAdapterDocument wifiAdapter = _wifiAdapterRepository.GetById(new DocumentId("33446"));
        XmpDocument xmp = _xmpRepository.GetById(new DocumentId("54322"));

        PcAssemblyValidatorException exception = Assert.Throws<PcAssemblyValidatorException>(() =>
        {
            PcAssembly assembly = new PcAssemblyBuilder().Init().WithMotherboard(motherboard).WithCpu(cpu)
                .WithProcessorCoolingSystem(coolingSystem).WithRam(ram).WithBios(bios).WithXmp(xmp)
                .WithVideoCard(videoCard)
                .WithHdd(hdd).WithSsd(ssd).WithPcCase(pcCase).WithPowerUnit(powerUnit).WithWifiAdapter(wifiAdapter)
                .Build();
        });
        Assert.Equal("Motherboard not supports installed BIOS", exception.Message);
    }
}