using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public class EImpulseEngine : ImpulseEngine
{
    private const double EImpulseEngineFuelConsumption = 1.1;
    private const double EImpulseEngineEpsilon = 0.001;
    private const double EImpulseEngineUpperbound = 700;
    private const double EImpulseEngineMaxIterations = 1000;

    public EImpulseEngine(double initialFuelLevel)
        : base(initialFuelLevel) { }

    public override double FuelConsumption => EImpulseEngineFuelConsumption;

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
        double time = EImpulseEngineUpperbound;
        int iteration = 0;

        while (Math.Abs(function(time)) > EImpulseEngineEpsilon &&
               iteration < EImpulseEngineMaxIterations)
        {
            time -= function(time) / functionDerivative(time);
            iteration++;
        }

        return time;
    }

    private static double GetTimeFromVelocity(double velocity)
    {
        return Math.Log(velocity + (1 + EImpulseEngineEpsilon));
    }

    private static double GetTimeFromLength(double length, double startTime)
    {
        return NewtonRaphsonMethod(Function, FunctionDerivative);

        double FunctionDerivative(double time) => Math.Exp(time) - (1 + EImpulseEngineEpsilon);

        double Function(double time) =>
            ((Math.Exp(time) - Math.Exp(startTime)) + ((1 + EImpulseEngineEpsilon) * (startTime - time))) - length;
    }
}