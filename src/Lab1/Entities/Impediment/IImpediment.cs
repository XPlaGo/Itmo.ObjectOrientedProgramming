using System;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public interface IImpediment : ICloneable
{
    double DamagePoints { get; }
    double DecreasePoints(double value);
    double IncreasePoints(double value);
    T AcceptImpedimentService<T>(IImpedimentVisitor<T> visitor);
}