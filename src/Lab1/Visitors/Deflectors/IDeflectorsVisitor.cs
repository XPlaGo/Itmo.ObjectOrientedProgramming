using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

public interface IDeflectorsVisitor<out T>
{
    T Visit(FirstClassDeflector deflector);
    T Visit(SecondClassDeflector deflector);
    T Visit(ThirdClassDeflector deflector);
    T Visit(PhotonDeflector deflector);
}