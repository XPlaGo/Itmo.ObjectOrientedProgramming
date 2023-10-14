using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public class PhotonDeflector : IDeflector
{
    public PhotonDeflector(double deflectorPoints, double photonPoints)
    {
        DeflectorPoints = deflectorPoints;
        PhotonPoints = photonPoints;
    }

    public double PhotonPoints { get; set; }

    public double DeflectorPoints { get; set; }

    public double Effectiveness => DeflectorsConfig.PhotonClassDeflectorEffectiveness;

    public T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}