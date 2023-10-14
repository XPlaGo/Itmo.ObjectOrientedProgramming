using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.ArmorsProtection;

public interface IArmorProtectionBuilder<TProtectionResult, out TArmor>
{
    IArmorProtectionBuilder<TProtectionResult, TArmor> FromMeteorites(Func<TArmor, Meteorites, TProtectionResult> protect);
    IArmorProtectionBuilder<TProtectionResult, TArmor> FromAntimatterFlares(Func<TArmor, AntimatterFlares, TProtectionResult> protect);
    IArmorProtectionBuilder<TProtectionResult, TArmor> FromCosmoWhales(Func<TArmor, CosmoWhales, TProtectionResult> protect);

    ArmorDispatcher<TProtectionResult> Confirm();
}