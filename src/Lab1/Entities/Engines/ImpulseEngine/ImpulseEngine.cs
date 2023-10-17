using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public abstract class ImpulseEngine : IEngine
{
    private const double ImpulseEngineFuelMaxLevel = 20;

    private double _fuelLevel;

    protected ImpulseEngine(double initialFuelLevel)
    {
        FuelLevel = initialFuelLevel;
    }

    public double CurrentVelocity { get; private set; }

    public abstract double FuelConsumption { get; }

    public double FuelLevel
    {
        get => _fuelLevel;
        set
        {
            if (value is < 0 or > ImpulseEngineFuelMaxLevel)
                throw new FuelException(value, 0, ImpulseEngineFuelMaxLevel);
            _fuelLevel = value;
        }
    }

    public double LaunchTime => Math.E;

    public double FillUpFuelLevel(double fillUpValue)
    {
        return FuelLevel += fillUpValue;
    }

    public abstract FlightResultResponse Fly(IEnvironment environment, double effectiveness);

    public abstract T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor);

    public void UpdateVelocity(double value)
    {
        CurrentVelocity = value;
    }
}