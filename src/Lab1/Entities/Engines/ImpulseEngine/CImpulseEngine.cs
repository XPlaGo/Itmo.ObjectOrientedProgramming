using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public class CImpulseEngine : ImpulseEngine
{
    public CImpulseEngine(double initialFuelLevel)
        : base(initialFuelLevel) { }

    public override double FuelConsumption => EnginesConfig.CImpulseEngineFuelConsumption;

    public override T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Fly(this);
    }

    public override FlightResultResponse Fly(IEnvironment environment, double effectiveness)
    {
        ArgumentNullException.ThrowIfNull(environment);
        if (FuelLevel == 0) return new FlightResultResponse(FlightResult.FuelRanOut);

        double effectiveLength = environment.Length / effectiveness;

        if (effectiveLength * FuelConsumption > FuelLevel)
        {
            double partiallyFlightLength = FuelLevel / FuelConsumption;
            double partiallyFlightTime = FlightTime(partiallyFlightLength);
            FuelLevel -= partiallyFlightLength * FuelConsumption;
            return new FlightResultResponse(
                FlightResult.PartiallyOvercome,
                new FlightResultData(
                    partiallyFlightTime,
                    CurrentVelocity,
                    partiallyFlightLength * effectiveness,
                    partiallyFlightLength * FuelConsumption));
        }

        double flightTime = FlightTime(effectiveLength);
        FuelLevel -= effectiveLength * FuelConsumption;

        return new FlightResultResponse(
            FlightResult.Overcome,
            new FlightResultData(
                flightTime,
                CurrentVelocity,
                environment.Length,
                effectiveLength * FuelConsumption));
    }

    private double FlightTime(double length)
    {
        return length / CurrentVelocity;
    }
}