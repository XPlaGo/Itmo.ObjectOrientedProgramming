using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

public interface IEnvironment : ICloneable
{
    public IList<IImpediment> Impediments { get; }

    public double Length { get; set; }

    public T AcceptEnvironmentVisitor<T>(IEnvironmentVisitor<T> visitor);
}