using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public class FirstClassDeflector : IDeflector
{
    private const double FirstClassDeflectorEffectiveness = 1.0;

    public FirstClassDeflector(double deflectorPoints)
    {
        DoubleValidationException.ThrowIfLessThan(deflectorPoints, 0);
        DeflectorPoints = deflectorPoints;
    }

    public double DeflectorPoints { get; private set; }

    public double Effectiveness => FirstClassDeflectorEffectiveness;

    public void DecreaseDeflectorPoints(double value)
    {
        DoubleValidationException.ThrowIfGreaterThan(value, DeflectorPoints);
        DeflectorPoints -= value;
    }

    public T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}