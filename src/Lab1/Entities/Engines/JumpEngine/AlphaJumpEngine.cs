using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

public class AlphaJumpEngine : JumpEngine
{
    private const double AlphaJumpEngineFuelConsumption = 1;

    public AlphaJumpEngine(double initialFuelLevel)
        : base(initialFuelLevel) { }

    public override double FuelConsumption => AlphaJumpEngineFuelConsumption;

    public override T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Fly(this);
    }

    public override FlightResultResponse Fly(IEnvironment environment, double effectiveness)
    {
        ArgumentNullException.ThrowIfNull(environment);
        if (FuelLevel == 0) return new FlightResultResponse(FlightResult.FuelRanOut, new FlightResultData(0, CurrentVelocity, 0, 0));

        double effectiveLength = environment.Length / effectiveness;
        double startTime = CurrentVelocity;

        if (effectiveLength * FuelConsumption > FuelLevel)
        {
            double partiallyFlightLength = FuelLevel / FuelConsumption;
            double partiallyEndTime = GetTime(partiallyFlightLength);
            double partiallyVelocity = partiallyEndTime;
            FuelLevel -= partiallyFlightLength * FuelConsumption;
            return new FlightResultResponse(
                FlightResult.PartiallyOvercome,
                new FlightResultData(
                    partiallyEndTime - startTime,
                    partiallyVelocity,
                    partiallyFlightLength * effectiveness,
                    partiallyFlightLength * FuelConsumption));
        }

        double endTime = GetTime(effectiveLength);
        double resultVelocity = endTime;
        FuelLevel -= effectiveLength * FuelConsumption;

        return new FlightResultResponse(
            FlightResult.Overcome,
            new FlightResultData(
                endTime - startTime,
                resultVelocity,
                environment.Length,
                effectiveLength * FuelConsumption));
    }

    private double GetTime(double length)
    {
        return Math.Sqrt((2.0 * length) + (CurrentVelocity * CurrentVelocity));
    }
}