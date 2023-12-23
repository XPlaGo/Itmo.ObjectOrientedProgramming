using System;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class WifiAdapterDocument : IDocument
{
    public WifiAdapterDocument(
        DocumentId id,
        string wifiStandardVersion,
        bool hasBuiltinBluetoothModule,
        string pcieVersion,
        float powerConsumption)
    {
        Id = id;
        WifiStandardVersion = wifiStandardVersion;
        HasBuiltinBluetoothModule = hasBuiltinBluetoothModule;
        PcieVersion = pcieVersion;
        PowerConsumption = powerConsumption;
    }

    public DocumentId Id { get; init; }
    public string WifiStandardVersion { get; init; }
    public bool HasBuiltinBluetoothModule { get; init; }
    public string PcieVersion { get; init; }
    public float PowerConsumption { get; init; }

    public static WifiAdapterDocument CopyWith(
        WifiAdapterDocument other,
        DocumentId? id = null,
        string? wifiStandardVersion = null,
        bool? hasBuiltinBluetoothModule = null,
        string? pcieVersion = null,
        float? powerConsumption = null)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new WifiAdapterDocument(
            id ?? other.Id,
            wifiStandardVersion ?? other.WifiStandardVersion,
            hasBuiltinBluetoothModule ?? other.HasBuiltinBluetoothModule,
            pcieVersion ?? other.PcieVersion,
            powerConsumption ?? other.PowerConsumption);
    }
}