﻿using System;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;

public class CosmoWhales : IImpediment
{
    public CosmoWhales(double damagePoints)
    {
        DamagePoints = damagePoints;
    }

    public double DamagePoints { get; set; }

    public T AcceptImpedimentService<T>(IImpedimentVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }

    public object Clone()
    {
        return new CosmoWhales(DamagePoints);
    }
}