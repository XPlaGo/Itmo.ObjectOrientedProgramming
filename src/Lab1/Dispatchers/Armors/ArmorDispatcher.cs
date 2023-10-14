using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.ArmorsProtection;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Armors;

public class ArmorDispatcher<TProtectionResult> : IArmorVisitor<IImpedimentVisitor<TProtectionResult>>
{
    private readonly ArmorProtectionBuilder<TProtectionResult, FirstArmorClass> _firstClassArmorBuilder;
    private readonly ArmorProtectionBuilder<TProtectionResult, SecondArmorClass> _secondClassArmorBuilder;
    private readonly ArmorProtectionBuilder<TProtectionResult, ThirdArmorClass> _thirdClassArmorBuilder;

    public ArmorDispatcher(Func<IArmor, IImpediment, TProtectionResult> protect)
    {
        _firstClassArmorBuilder = new ArmorProtectionBuilder<TProtectionResult, FirstArmorClass>(this, protect);
        _secondClassArmorBuilder = new ArmorProtectionBuilder<TProtectionResult, SecondArmorClass>(this, protect);
        _thirdClassArmorBuilder = new ArmorProtectionBuilder<TProtectionResult, ThirdArmorClass>(this, protect);
    }

    public IArmorProtectionBuilder<TProtectionResult, FirstArmorClass> UseFirstClassArmor => _firstClassArmorBuilder;

    public IArmorProtectionBuilder<TProtectionResult, SecondArmorClass> UseSecondClassArmor => _secondClassArmorBuilder;

    public IArmorProtectionBuilder<TProtectionResult, ThirdArmorClass> UseThirdClassArmor => _thirdClassArmorBuilder;

    public IImpedimentVisitor<TProtectionResult> Visit(FirstArmorClass armor)
    {
        return _firstClassArmorBuilder.Armor(armor);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(SecondArmorClass armor)
    {
        return _secondClassArmorBuilder.Armor(armor);
    }

    public IImpedimentVisitor<TProtectionResult> Visit(ThirdArmorClass armor)
    {
        return _thirdClassArmorBuilder.Armor(armor);
    }
}