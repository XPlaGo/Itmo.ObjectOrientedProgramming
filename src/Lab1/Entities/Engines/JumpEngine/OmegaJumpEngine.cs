using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

public class OmegaJumpEngine : JumpEngine
{
    private const double OmegaJumpEngineFuelConsumption = 1;
    private const double OmegaJumpEngineEpsilon = 0.001;
    private const double OmegaJumpEngineUpperbound = 1e6;
    private const double OmegaJumpEngineMaxIterations = 100;

    public OmegaJumpEngine(double initialFuelLevel)
        : base(initialFuelLevel)
    {
    }

    public override double FuelConsumption => OmegaJumpEngineFuelConsumption;

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
        double startTime = GetTimeFromVelocity(CurrentVelocity);

        if (effectiveLength * FuelConsumption > FuelLevel)
        {
            double partiallyFlightLength = FuelLevel / FuelConsumption;
            double partiallyEndTime = GetTimeFromLength(partiallyFlightLength, startTime);
            double partiallyVelocity = GetVelocityFromTime(partiallyEndTime);
            FuelLevel -= partiallyFlightLength * FuelConsumption;
            return new FlightResultResponse(
                FlightResult.PartiallyOvercome,
                new FlightResultData(
                    partiallyEndTime - startTime,
                    partiallyVelocity,
                    partiallyFlightLength * effectiveness,
                    partiallyFlightLength * FuelConsumption));
        }

        double endTime = GetTimeFromLength(effectiveLength, startTime);
        double resultVelocity = GetVelocityFromTime(endTime);
        FuelLevel -= effectiveLength * FuelConsumption;

        return new FlightResultResponse(
            FlightResult.Overcome,
            new FlightResultData(
                endTime - startTime,
                resultVelocity,
                environment.Length,
                effectiveLength * FuelConsumption));
    }

    private static double GetVelocityFromTime(double time)
    {
        return time * Math.Log(time, Math.E);
    }

    private static double NewtonRaphsonMethod(
        Func<double, double> function,
        Func<double, double> functionDerivative)
    {
        double time = OmegaJumpEngineUpperbound;
        int iteration = 0;

        while (Math.Abs(function(time)) > OmegaJumpEngineEpsilon &&
               iteration < OmegaJumpEngineMaxIterations)
        {
            time -= function(time) / functionDerivative(time);
            iteration++;
        }

        return time;
    }

    private static double GetTimeFromVelocity(double velocity)
    {
        return NewtonRaphsonMethod(Function, FunctionDerivative);

        double Function(double time) => (time * Math.Log(time)) - velocity;
        double FunctionDerivative(double time) => Math.Log(time) + 1;
    }

    private static double GetTimeFromLength(double length, double startTime)
    {
        return NewtonRaphsonMethod(Function, FunctionDerivative);

        double FunctionDerivative(double time) => time * Math.Log(time, Math.E);

        double Function(double time) =>
            (0.5 * ((Math.Log(time, Math.E) * time * time) - (Math.Log(startTime, Math.E) * startTime * startTime))) +
            (0.25 * ((-time * time) + (startTime * startTime))) - length;
    }
}