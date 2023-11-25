using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.EmittersProtection;

public interface IEmitterProtectionBuilder<TProtectionResult, out TEmitter>
{
    IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromMeteorites(Func<TEmitter, Meteorites, TProtectionResult> protect);
    IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromAntimatterFlares(Func<TEmitter, AntimatterFlares, TProtectionResult> protect);
    IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromCosmoWhales(Func<TEmitter, CosmoWhales, TProtectionResult> protect);
    EmitterDispatcher<TProtectionResult> Confirm();
}