using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public class EImpulseEngine : ImpulseEngine
{
    public EImpulseEngine(double initialFuelLevel)
        : base(initialFuelLevel) { }

    public override double FuelConsumption => EnginesConfig.EImpulseEngineFuelConsumption;

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
        double startTime = GetTimeFromVelocity(CurrentVelocity);

        if (effectiveLength * FuelConsumption > FuelLevel)
        {
            double partiallyFlightLength = FuelLevel / FuelConsumption;
            double partiallyEndTime = GetTimeFromLength(partiallyFlightLength, startTime);
            double partiallyVelocity = Math.Exp(partiallyEndTime) - 1;
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
        double resultVelocity = Math.Exp(endTime) - 1;
        FuelLevel -= effectiveLength * FuelConsumption;

        return new FlightResultResponse(
            FlightResult.Overcome,
            new FlightResultData(
                endTime - startTime,
                resultVelocity,
                environment.Length,
                effectiveLength * FuelConsumption));
    }

    private static double NewtonRaphsonMethod(
        Func<double, double> function,
        Func<double, double> functionDerivative)
    {
        double t = EnginesConfig.EImpulseEngineUpperbound;
        int i = 0;

        while (Math.Abs(function(t)) > EnginesConfig.EImpulseEngineEpsilon &&
               i < EnginesConfig.EImpulseEngineMaxIterations)
        {
            double a = function(t);
            double b = functionDerivative(t);
            t -= a / b;
            i++;
        }

        return t;
    }

    private static double GetTimeFromVelocity(double velocity)
    {
        return Math.Log(velocity + (1 + EnginesConfig.OmegaJumpEngineEpsilon));
    }

    private static double GetTimeFromLength(double length, double startTime)
    {
        return NewtonRaphsonMethod(Function, FunctionDerivative);

        double FunctionDerivative(double time) => Math.Exp(time) - (1 + EnginesConfig.OmegaJumpEngineEpsilon);

        double Function(double time) =>
            ((Math.Exp(time) - Math.Exp(startTime)) + ((1 + EnginesConfig.OmegaJumpEngineEpsilon) * (startTime - time))) - length;
    }
}