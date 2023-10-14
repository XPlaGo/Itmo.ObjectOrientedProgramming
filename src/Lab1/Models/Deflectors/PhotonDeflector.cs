using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public class PhotonDeflector : IDeflector
{
    private const double PhotonClassDeflectorEffectiveness = 1.0;

    public PhotonDeflector(double deflectorPoints, double photonPoints)
    {
        DoubleValidationException.ThrowIfLessThan(deflectorPoints, 0);
        DoubleValidationException.ThrowIfLessThan(photonPoints, 0);
        DeflectorPoints = deflectorPoints;
        PhotonPoints = photonPoints;
    }

    public double PhotonPoints { get; private set; }

    public double DeflectorPoints { get; private set; }

    public double Effectiveness => PhotonClassDeflectorEffectiveness;

    public void DecreaseDeflectorPoints(double value)
    {
        DoubleValidationException.ThrowIfGreaterThan(value, DeflectorPoints);
        DeflectorPoints -= value;
    }

    public void DecreasePhotonPoints(double value)
    {
        DoubleValidationException.ThrowIfGreaterThan(value, PhotonPoints);
        PhotonPoints -= value;
    }

    public T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}