using System;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public interface IImpediment : ICloneable
{
    double DamagePoints { get; set; }
    T AcceptImpedimentService<T>(IImpedimentVisitor<T> visitor);
}