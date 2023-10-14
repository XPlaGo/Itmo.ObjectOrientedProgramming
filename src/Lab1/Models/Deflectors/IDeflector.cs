using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public interface IDeflector
{
    double DeflectorPoints { get; }
    double Effectiveness { get; }

    void DecreaseDeflectorPoints(double value);
    T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor);
}