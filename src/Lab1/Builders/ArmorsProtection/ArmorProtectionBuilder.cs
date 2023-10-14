using System;
using System.Data;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.ArmorsProtection;

public class ArmorProtectionBuilder<TProtectionResult, TArmor> : IArmorProtectionBuilder<TProtectionResult, TArmor>,
    IImpedimentVisitor<TProtectionResult>
    where TArmor : IArmor
{
#pragma warning disable SK1400
    private readonly ArmorDispatcher<TProtectionResult> _dispatcher;
#pragma warning restore SK1400

    private readonly Func<TArmor, IImpediment, TProtectionResult> _protect;
    private Func<TArmor, Meteorites, TProtectionResult> _protectMeteorites;
    private Func<TArmor, AntimatterFlares, TProtectionResult> _protectAntimatterFlares;
    private Func<TArmor, CosmoWhales, TProtectionResult> _protectCosmoWhales;

    private TArmor? _armor;

    public ArmorProtectionBuilder(
        ArmorDispatcher<TProtectionResult> dispatcher,
        Func<TArmor, IImpediment, TProtectionResult> protect)
    {
        _dispatcher = dispatcher;
        _protect = protect;

        _protectMeteorites = (d, i) => protect(d, i);
        _protectAntimatterFlares = (d, i) => protect(d, i);
        _protectCosmoWhales = (d, i) => protect(d, i);
    }

    public IImpedimentVisitor<TProtectionResult> Armor(TArmor armor)
    {
        _armor = armor;
        return this;
    }

    public IArmorProtectionBuilder<TProtectionResult, TArmor> FromMeteorites(Func<TArmor, Meteorites, TProtectionResult> protect)
    {
        _protectMeteorites = protect;
        return this;
    }

    public IArmorProtectionBuilder<TProtectionResult, TArmor> FromAntimatterFlares(Func<TArmor, AntimatterFlares, TProtectionResult> protect)
    {
        _protectAntimatterFlares = protect;
        return this;
    }

    public IArmorProtectionBuilder<TProtectionResult, TArmor> FromCosmoWhales(Func<TArmor, CosmoWhales, TProtectionResult> protect)
    {
        _protectCosmoWhales = protect;
        return this;
    }

    #pragma warning disable SK1400
    public ArmorDispatcher<TProtectionResult> Confirm()
    {
        return _dispatcher;
    }
    #pragma warning restore SK1400

    public TProtectionResult Visit(Meteorites impediment)
    {
        if (_armor == null) throw new NoNullAllowedException(nameof(_armor));
        return _protectMeteorites(_armor, impediment);
    }

    public TProtectionResult Visit(AntimatterFlares impediment)
    {
        if (_armor == null) throw new NoNullAllowedException(nameof(_armor));
        return _protectAntimatterFlares(_armor, impediment);
    }

    public TProtectionResult Visit(CosmoWhales impediment)
    {
        if (_armor == null) throw new NoNullAllowedException(nameof(_armor));
        return _protectCosmoWhales(_armor, impediment);
    }
}