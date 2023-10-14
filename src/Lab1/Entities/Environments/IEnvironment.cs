using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

public interface IEnvironment : ICloneable
{
    public IReadOnlyCollection<IImpediment> Impediments { get; }

    public double Length { get; }

    public double DecreaseLength(double value);

    public T AcceptEnvironmentVisitor<T>(IEnvironmentVisitor<T> visitor);
}