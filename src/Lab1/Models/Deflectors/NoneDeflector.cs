using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Components;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public class NoneDeflector : IDeflector
{
    public double DeflectorPoints { get; }
    public double Effectiveness { get; }
    public void DecreaseDeflectorPoints(double value)
    {
        throw new CannotUseNoneComponentException(GetType().Name);
    }

    public T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}