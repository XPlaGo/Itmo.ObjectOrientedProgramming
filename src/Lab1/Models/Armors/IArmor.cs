using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;

public interface IArmor
{
    double ArmorPoints { get; set; }
    double OverallCharacteristics { get; init; }
    T AcceptArmorVisitor<T>(IArmorVisitor<T> visitor);
}