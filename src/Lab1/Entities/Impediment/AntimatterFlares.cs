using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public class AntimatterFlares : IImpediment
{
    public AntimatterFlares(double damagePoints)
    {
        DoubleValidationException.ThrowIfLessThan(damagePoints, 0);
        DamagePoints = damagePoints;
    }

    public double DamagePoints { get; private set; }
    public double DecreasePoints(double value)
    {
        DoubleValidationException.ThrowIfGreaterThan(value, DamagePoints);
        DamagePoints -= value;
        return DamagePoints;
    }

    public double IncreasePoints(double value)
    {
        DoubleValidationException.ThrowIfLessThan(value, -DamagePoints);
        DamagePoints += value;
        return DamagePoints;
    }

    public T AcceptImpedimentService<T>(IImpedimentVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }

    public object Clone()
    {
        return new AntimatterFlares(DamagePoints);
    }
}