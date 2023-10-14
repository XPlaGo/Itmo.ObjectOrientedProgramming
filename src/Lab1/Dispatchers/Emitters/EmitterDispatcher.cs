using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.EmittersProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Emitters;

public class EmitterDispatcher<TProtectionResult> : IEmitterVisitor<IImpedimentVisitor<TProtectionResult>>
{
    private readonly EmitterProtectionBuilder<TProtectionResult, AntiNeutrinoEmitter> _antiNeutrinoEmitterBuilder;

    public EmitterDispatcher(Func<IEmitter, IImpediment, TProtectionResult> protect)
    {
        _antiNeutrinoEmitterBuilder = new EmitterProtectionBuilder<TProtectionResult, AntiNeutrinoEmitter>(this, protect);
    }

    public IEmitterProtectionBuilder<TProtectionResult, AntiNeutrinoEmitter> UseAntiNeutrinoEmitter => _antiNeutrinoEmitterBuilder;

    public IImpedimentVisitor<TProtectionResult> Visit(AntiNeutrinoEmitter emitter)
    {
        return _antiNeutrinoEmitterBuilder.Emitter(emitter);
    }
}