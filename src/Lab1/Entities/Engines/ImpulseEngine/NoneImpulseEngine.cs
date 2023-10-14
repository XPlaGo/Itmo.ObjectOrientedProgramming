using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Components;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public class NoneImpulseEngine : ImpulseEngine
{
    public NoneImpulseEngine()
        : base(0) { }

    public override double FuelConsumption => 0;
    public override FlightResultResponse Fly(IEnvironment environment, double effectiveness)
    {
        throw new CannotUseNoneComponentException(this.GetType().Name);
    }

    public override T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Fly(this);
    }
}