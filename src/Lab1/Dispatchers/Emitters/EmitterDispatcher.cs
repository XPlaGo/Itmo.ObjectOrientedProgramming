using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.EmittersProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Emitters;

public class EmitterDispatcher<TProtectionResult> : IEmitterVisitor<IImpedimentVisitor<TProtectionResult>>
{
    public EmitterDispatcher(Func<IEmitter, IImpediment, TProtectionResult> protect)
    {
        UseAntiNeutrinoEmitter = new EmitterProtectionBuilder<TProtectionResult, AntiNeutrinoEmitter>(this, protect);
        UseNoneEmitter = new EmitterProtectionBuilder<TProtectionResult, NoneEmitter>(this, protect);
    }

    public EmitterProtectionBuilder<TProtectionResult, AntiNeutrinoEmitter> UseAntiNeutrinoEmitter { get; }
    public EmitterProtectionBuilder<TProtectionResult, NoneEmitter> UseNoneEmitter { get; }

    public IImpedimentVisitor<TProtectionResult> Visit(AntiNeutrinoEmitter emitter)
    {
        return UseAntiNeutrinoEmitter.Emitter(emitter);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(NoneEmitter emitter)
    {
        return UseNoneEmitter.Emitter(emitter);
    }
}