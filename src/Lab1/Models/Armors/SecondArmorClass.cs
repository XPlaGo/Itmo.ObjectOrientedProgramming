using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;

public class SecondArmorClass : IArmor
{
    private const double SecondArmorPoints = 200;

    public SecondArmorClass(double overallCharacteristics, double armorPoints = SecondArmorPoints)
    {
        DoubleValidationException.ThrowIfLessOrEqThan(overallCharacteristics, 0);
        DoubleValidationException.ThrowIfLessThan(armorPoints, 0);
        OverallCharacteristics = overallCharacteristics;
        ArmorPoints = armorPoints;
    }

    public double ArmorPoints { get; private set; }
    public double OverallCharacteristics { get; init; }

    public void DecreaseArmorPoints(double value)
    {
        DoubleValidationException.ThrowIfGreaterThan(value, ArmorPoints);
        ArmorPoints -= value;
    }

    public T AcceptArmorVisitor<T>(IArmorVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}