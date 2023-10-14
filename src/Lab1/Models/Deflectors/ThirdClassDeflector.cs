﻿using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;

public class ThirdClassDeflector : IDeflector
{
    public ThirdClassDeflector(double deflectorPoints)
    {
        DeflectorPoints = deflectorPoints;
    }

    public double DeflectorPoints { get; set; }

    public double Effectiveness => DeflectorsConfig.ThirdClassDeflectorEffectiveness;

    public T AcceptDeflectorVisitor<T>(IDeflectorsVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}