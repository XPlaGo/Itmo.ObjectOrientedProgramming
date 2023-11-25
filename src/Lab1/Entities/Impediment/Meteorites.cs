using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public class Meteorites : IImpediment
{
    public Meteorites(double damagePoints, double overallCharacteristics)
    {
        DoubleValidationException.ThrowIfLessOrEqThan(overallCharacteristics, 0);
        DoubleValidationException.ThrowIfLessThan(damagePoints, 0);
        DamagePoints = damagePoints;
        OverallCharacteristics = overallCharacteristics;
    }

    public double DamagePoints { get; private set; }
    public double OverallCharacteristics { get; }

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
        return new Meteorites(DamagePoints, OverallCharacteristics);
    }
}