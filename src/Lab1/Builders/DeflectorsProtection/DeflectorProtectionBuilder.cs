using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Builders;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.DeflectorsProtection;

public class DeflectorProtectionBuilder<TProtectionResult, TDeflector> : IDeflectorProtectionBuilder<TProtectionResult, TDeflector>,
    IImpedimentVisitor<TProtectionResult>
    where TDeflector : IDeflector
{
    private readonly Func<TDeflector, IImpediment, TProtectionResult> _protect;
    private Func<TDeflector, Meteorites, TProtectionResult> _protectMeteorites;
    private Func<TDeflector, AntimatterFlares, TProtectionResult> _protectAntimatterFlares;
    private Func<TDeflector, CosmoWhales, TProtectionResult> _protectCosmoWhales;

    private TDeflector? _deflector;

    public DeflectorProtectionBuilder(
        DeflectorDispatcher<TProtectionResult> dispatcher,
        Func<TDeflector, IImpediment, TProtectionResult> protect)
    {
        Dispatcher = dispatcher;
        _protect = protect;

        _protectMeteorites = (deflector, impediment) => protect(deflector, impediment);
        _protectAntimatterFlares = (deflector, impediment) => protect(deflector, impediment);
        _protectCosmoWhales = (deflector, impediment) => protect(deflector, impediment);
    }

    private DeflectorDispatcher<TProtectionResult> Dispatcher { get; }

    public IImpedimentVisitor<TProtectionResult> Deflector(TDeflector deflector)
    {
        _deflector = deflector;
        return this;
    }

    public IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromMeteorites(Func<TDeflector, Meteorites, TProtectionResult> protect)
    {
        _protectMeteorites = protect;
        return this;
    }

    public IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromAntimatterFlares(Func<TDeflector, AntimatterFlares, TProtectionResult> protect)
    {
        _protectAntimatterFlares = protect;
        return this;
    }

    public IDeflectorProtectionBuilder<TProtectionResult, TDeflector> FromCosmoWhales(Func<TDeflector, CosmoWhales, TProtectionResult> protect)
    {
        _protectCosmoWhales = protect;
        return this;
    }

    public DeflectorDispatcher<TProtectionResult> Confirm()
    {
        return Dispatcher;
    }

    public TProtectionResult Visit(Meteorites impediment)
    {
        if (_deflector is null) throw new BuildComponentCannotBeNull(nameof(_deflector));
        return _protectMeteorites(_deflector, impediment);
    }

    public TProtectionResult Visit(AntimatterFlares impediment)
    {
        if (_deflector is null) throw new BuildComponentCannotBeNull(nameof(_deflector));
        return _protectAntimatterFlares(_deflector, impediment);
    }

    public TProtectionResult Visit(CosmoWhales impediment)
    {
        if (_deflector is null) throw new BuildComponentCannotBeNull(nameof(_deflector));
        return _protectCosmoWhales(_deflector, impediment);
    }
}