using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;

public interface IArmor
{
    double ArmorPoints { get; }
    double OverallCharacteristics { get; }

    void DecreaseArmorPoints(double value);
    T AcceptArmorVisitor<T>(IArmorVisitor<T> visitor);
}