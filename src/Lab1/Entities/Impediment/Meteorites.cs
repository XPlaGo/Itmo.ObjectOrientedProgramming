using System;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public class Meteorites : IImpediment
{
    public Meteorites(double damagePoints, double overallCharacteristics)
    {
        DamagePoints = damagePoints;
        OverallCharacteristics = overallCharacteristics;
    }

    public double DamagePoints { get; set; }
    public double OverallCharacteristics { get; init; }

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