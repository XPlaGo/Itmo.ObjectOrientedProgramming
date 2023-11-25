using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.DeflectorsProtection;

public interface IDeflectorProtectionBuilder<TProtectionResult, out TDeflector>
{
    IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromMeteorites(Func<TDeflector, Meteorites, TProtectionResult> protect);
    IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromAntimatterFlares(Func<TDeflector, AntimatterFlares, TProtectionResult> protect);
    IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromCosmoWhales(Func<TDeflector, CosmoWhales, TProtectionResult> protect);
    DeflectorDispatcher<TProtectionResult> Confirm();
}