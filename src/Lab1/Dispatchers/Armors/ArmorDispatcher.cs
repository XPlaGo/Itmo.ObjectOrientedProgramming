using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.ArmorsProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Armors;

public class ArmorDispatcher<TProtectionResult> : IArmorVisitor<IImpedimentVisitor<TProtectionResult>>
{
    public ArmorDispatcher(Func<IArmor, IImpediment, TProtectionResult> protect)
    {
        UseFirstClassArmor = new ArmorProtectionBuilder<TProtectionResult, FirstArmorClass>(this, protect);
        UseSecondClassArmor = new ArmorProtectionBuilder<TProtectionResult, SecondArmorClass>(this, protect);
        UseThirdClassArmor = new ArmorProtectionBuilder<TProtectionResult, ThirdArmorClass>(this, protect);
    }

    public ArmorProtectionBuilder<TProtectionResult, FirstArmorClass> UseFirstClassArmor { get; }
    public ArmorProtectionBuilder<TProtectionResult, SecondArmorClass> UseSecondClassArmor { get; }
    public ArmorProtectionBuilder<TProtectionResult, ThirdArmorClass> UseThirdClassArmor { get; }

    public IImpedimentVisitor<TProtectionResult> Visit(FirstArmorClass armor)
    {
        return UseFirstClassArmor.Armor(armor);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(SecondArmorClass armor)
    {
        return UseSecondClassArmor.Armor(armor);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(ThirdArmorClass armor)
    {
        return UseThirdClassArmor.Armor(armor);
    }
}