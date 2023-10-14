using System;
using System.Data;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.EmittersProtection;

public class EmitterProtectionBuilder<TProtectionResult, TEmitter> : IEmitterProtectionBuilder<TProtectionResult, TEmitter>,
    IImpedimentVisitor<TProtectionResult>
    where TEmitter : IEmitter
{
    #pragma warning disable SK1400
    private readonly EmitterDispatcher<TProtectionResult> _dispatcher;
    #pragma warning restore SK1400

    private readonly Func<TEmitter, IImpediment, TProtectionResult> _protect;
    private Func<TEmitter, Meteorites, TProtectionResult> _protectMeteorites;
    private Func<TEmitter, AntimatterFlares, TProtectionResult> _protectAntimatterFlares;
    private Func<TEmitter, CosmoWhales, TProtectionResult> _protectCosmoWhales;

    private TEmitter? _emitter;

    public EmitterProtectionBuilder(
        EmitterDispatcher<TProtectionResult> dispatcher,
        Func<TEmitter, IImpediment, TProtectionResult> protect)
    {
        _dispatcher = dispatcher;
        _protect = protect;

        _protectMeteorites = (d, i) => protect(d, i);
        _protectAntimatterFlares = (d, i) => protect(d, i);
        _protectCosmoWhales = (d, i) => protect(d, i);
    }

    public IImpedimentVisitor<TProtectionResult> Emitter(TEmitter emitter)
    {
        _emitter = emitter;
        return this;
    }

    public IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromMeteorites(Func<TEmitter, Meteorites, TProtectionResult> protect)
    {
        _protectMeteorites = protect;
        return this;
    }

    public IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromAntimatterFlares(Func<TEmitter, AntimatterFlares, TProtectionResult> protect)
    {
        _protectAntimatterFlares = protect;
        return this;
    }

    public IEmitterProtectionBuilder<TProtectionResult, TEmitter> FromCosmoWhales(Func<TEmitter, CosmoWhales, TProtectionResult> protect)
    {
        _protectCosmoWhales = protect;
        return this;
    }

    #pragma warning disable SK1400
    public EmitterDispatcher<TProtectionResult> Confirm()
    {
        return _dispatcher;
    }
    #pragma warning restore SK1400

    public TProtectionResult Visit(Meteorites impediment)
    {
        if (_emitter == null) throw new NoNullAllowedException(nameof(_emitter));
        return _protectMeteorites(_emitter, impediment);
    }

    public TProtectionResult Visit(AntimatterFlares impediment)
    {
        if (_emitter == null) throw new NoNullAllowedException(nameof(_emitter));
        return _protectAntimatterFlares(_emitter, impediment);
    }

    public TProtectionResult Visit(CosmoWhales impediment)
    {
        if (_emitter == null) throw new NoNullAllowedException(nameof(_emitter));
        return _protectCosmoWhales(_emitter, impediment);
    }
}