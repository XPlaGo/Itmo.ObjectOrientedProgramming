using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public interface IDeflector
{
    double DeflectorPoints { get; set; }
    double Effectiveness { get; }
    T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor);
}