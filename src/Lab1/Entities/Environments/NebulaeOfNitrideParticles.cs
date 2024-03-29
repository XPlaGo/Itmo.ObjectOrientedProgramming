﻿using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

public class NebulaeOfNitrideParticles : IEnvironment
{
    private double _length;

    public NebulaeOfNitrideParticles(double length, IReadOnlyCollection<IImpediment> impediments)
    {
        Length = length;
        Impediments = impediments;
    }

    public IReadOnlyCollection<IImpediment> Impediments { get; }

    public double Length
    {
        get => _length;
        set
        {
            if (value < 0) throw new LengthException($"Length must be positive or 0, have: {value}");
            _length = value;
        }
    }

    public double DecreaseLength(double value)
    {
        Length -= value;
        return Length;
    }

    public T AcceptEnvironmentVisitor<T>(IEnvironmentVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }

    public object Clone()
    {
        return new NebulaeOfNitrideParticles(
            Length,
            Impediments.Select(i => (IImpediment)i.Clone()).ToList());
    }
}