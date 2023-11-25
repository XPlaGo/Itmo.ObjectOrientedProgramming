using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Validator;

namespace Itmo.ObjectOrientedProgramming.Lab2.Validators;

public static class PcAssemblyValidator
{
    public static void Validate(
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
        ValidatePowerConsumption(cpu, hdd, ssd, videoCard, powerUnit);
        ValidateCpuSocket(cpu, motherboard, processorCoolingSystem);
        ValidateCpuTdp(cpu, processorCoolingSystem);
        ValidateRamMemory(cpu, motherboard, ram);
        ValidateBios(cpu, motherboard, bios);
        ValidatePcCase(videoCard, pcCase, motherboard);
        ValidateXmp(xmp, motherboard, cpu);
        ValidateWifiAdapter(motherboard, wifiAdapter);
    }

    private static void ValidateWifiAdapter(
        MotherboardDocument motherboardDocument,
        WifiAdapterDocument? wifiAdapterDocument)
    {
        ArgumentNullException.ThrowIfNull(motherboardDocument);

        if (motherboardDocument.HasBuiltinWifiAdapter && wifiAdapterDocument is not null)
            throw new PcAssemblyValidatorException("Motherboard has built-in wifi adapter");
    }

    private static void ValidatePowerConsumption(
        CpuDocument cpu,
        HddDocument? hdd,
        SsdDocument? ssd,
        VideoCardDocument videoCard,
        PowerUnitDocument powerUnit)
    {
        ArgumentNullException.ThrowIfNull(cpu);
        ArgumentNullException.ThrowIfNull(videoCard);
        ArgumentNullException.ThrowIfNull(powerUnit);

        if (cpu.PowerConsumption + (hdd?.PowerConsumption ?? 0) + (ssd?.PowerConsumption ?? 0) +
            videoCard.PowerConsumption > powerUnit.MaxLoad)
        {
            throw new PcAssemblyValidatorException("Power consumption is more than power unit max load");
        }
    }

    private static void ValidateCpuSocket(
        CpuDocument cpuDocument,
        MotherboardDocument motherboardDocument,
        ProcessorCoolingSystemDocument processorCoolingSystemDocument)
    {
        ArgumentNullException.ThrowIfNull(cpuDocument);
        ArgumentNullException.ThrowIfNull(motherboardDocument);
        ArgumentNullException.ThrowIfNull(processorCoolingSystemDocument);

        if (!string.Equals(cpuDocument.Socket, motherboardDocument.Socket, StringComparison.Ordinal))
        {
            throw new PcAssemblyValidatorException("Socket of CPU is not supported by motherboard");
        }

        int commonSupportedSockets = (
            from socket in processorCoolingSystemDocument.SupportedSockets
            where socket == cpuDocument.Socket
            select socket).Count();

        if (commonSupportedSockets == 0)
        {
            throw new PcAssemblyValidatorException("Socket of CPU is not supported by processor cooling system");
        }
    }

    private static void ValidateCpuTdp(
        CpuDocument cpuDocument,
        ProcessorCoolingSystemDocument processorCoolingSystemDocument)
    {
        ArgumentNullException.ThrowIfNull(cpuDocument);
        ArgumentNullException.ThrowIfNull(processorCoolingSystemDocument);

        if (cpuDocument.Tdp > processorCoolingSystemDocument.Tdp)
        {
            throw new PcAssemblyValidatorException("CPU TDP is more than TDP of processor cooling system");
        }
    }

    private static void ValidateRamMemory(
        CpuDocument cpuDocument,
        MotherboardDocument motherboardDocument,
        RamDocument ramDocument)
    {
        ArgumentNullException.ThrowIfNull(cpuDocument);
        ArgumentNullException.ThrowIfNull(motherboardDocument);

        IEnumerable<string> supportedRamVersions =
            from cpuRamVersion in cpuDocument.SupportedMemoryVersions
            from mbRamVersion in motherboardDocument.SupportedMemoryVersions
            where cpuRamVersion == mbRamVersion && mbRamVersion == ramDocument.DdrVersion
            select cpuRamVersion;

        IEnumerable<string> ramVersions = supportedRamVersions.ToList();
        if (!ramVersions.Any())
        {
            throw new PcAssemblyValidatorException("CPU and motherboard don't have common RAM version");
        }

        int supportedRamVersionsCount = (
            from cpuRamVersion in ramVersions
            where cpuRamVersion == ramDocument.DdrVersion
            select cpuRamVersion).Count();

        if (supportedRamVersionsCount == 0)
        {
            throw new PcAssemblyValidatorException("Installed RAM version is not supported");
        }
    }

    private static void ValidateBios(
        CpuDocument cpuDocument,
        MotherboardDocument motherboardDocument,
        BiosDocument biosDocument)
    {
        ArgumentNullException.ThrowIfNull(motherboardDocument);
        ArgumentNullException.ThrowIfNull(biosDocument);

        if (!motherboardDocument.Bios.Id.Equals(biosDocument.Id))
        {
            throw new PcAssemblyValidatorException("Motherboard not supports installed BIOS");
        }

        int supportedCpuCount = (
            from cpuRef in biosDocument.CpuSupportedList
            where cpuRef.Id.Equals(cpuDocument.Id)
            select cpuRef).Count();

        if (supportedCpuCount == 0)
        {
            throw new PcAssemblyValidatorException("CPU not supports installed BIOS");
        }
    }

    private static void ValidatePcCase(
        VideoCardDocument videoCardDocument,
        PcCaseDocument pcCaseDocument,
        MotherboardDocument motherboardDocument)
    {
        ArgumentNullException.ThrowIfNull(videoCardDocument);
        ArgumentNullException.ThrowIfNull(pcCaseDocument);
        ArgumentNullException.ThrowIfNull(motherboardDocument);

        if (videoCardDocument.Height > pcCaseDocument.MaxVideoCardHeight ||
            videoCardDocument.Width > pcCaseDocument.MaxVideoCardWidth)
        {
            throw new PcAssemblyValidatorException("Video card size is not supported by pc case");
        }

        int supportFormFactorCount = (
            from formFactor in pcCaseDocument.SupportedMotherboardFormFactors
            where formFactor == motherboardDocument.FormFactor
            select formFactor).Count();

        if (supportFormFactorCount == 0)
        {
            throw new PcAssemblyValidatorException("Motherboard form factor is not supported by pc case");
        }
    }

    private static void ValidateXmp(
        XmpDocument xmpDocument,
        MotherboardDocument motherboardDocument,
        CpuDocument cpuDocument)
    {
        ArgumentNullException.ThrowIfNull(xmpDocument);
        ArgumentNullException.ThrowIfNull(motherboardDocument);
        ArgumentNullException.ThrowIfNull(cpuDocument);

        if (xmpDocument.Frequency != cpuDocument.CorsFrequency)
        {
            throw new PcAssemblyValidatorException("CPU don't support XMP frequency");
        }

        if (!motherboardDocument.XmpSupport)
        {
            throw new PcAssemblyValidatorException("Motherboard don't support XMP");
        }
    }
}