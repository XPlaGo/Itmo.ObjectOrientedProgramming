using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

public class GammaJumpEngine : JumpEngine
{
    private const double GammaJumpEngineFuelConsumption = 1;

    public GammaJumpEngine(double initialFuelLevel)
        : base(initialFuelLevel) { }

    public override double FuelConsumption => GammaJumpEngineFuelConsumption;

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
        double startTime = Math.Sqrt(CurrentVelocity);

        if (effectiveLength * FuelConsumption > FuelLevel)
        {
            double partiallyFlightLength = FuelLevel / FuelConsumption;
            double partiallyFlightTime = GetTime(partiallyFlightLength);
            FuelLevel -= partiallyFlightLength * FuelConsumption;
            return new FlightResultResponse(
                FlightResult.PartiallyOvercome,
                new FlightResultData(
                    partiallyFlightTime,
                    CurrentVelocity,
                    partiallyFlightLength * effectiveness,
                    partiallyFlightLength * FuelConsumption));
        }

        double endTime = GetTime(effectiveLength);
        double resultVelocity = endTime * endTime;
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
        return Math.Pow((3 * length) + Math.Pow(CurrentVelocity, 3.0 / 2.0), 1.0 / 3.0);
    }
}