using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;

public interface IArmorVisitor<out T>
{
    T Visit(FirstArmorClass armor);
    T Visit(SecondArmorClass armor);
    T Visit(ThirdArmorClass armor);
}