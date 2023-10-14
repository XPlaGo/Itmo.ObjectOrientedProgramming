using System;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;

public class FirstArmorClass : IArmor
{
    public FirstArmorClass(double overallCharacteristics, double armorPoints = 100)
    {
        OverallCharacteristics = overallCharacteristics;
        ArmorPoints = armorPoints;
    }

    public double ArmorPoints { get; set; }
    public double OverallCharacteristics { get; init; }

    public T AcceptArmorVisitor<T>(IArmorVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}