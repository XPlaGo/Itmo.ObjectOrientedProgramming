using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;

public abstract class ImpulseEngine : IEngine
{
    private static readonly double MaxFuelLevel = EnginesConfig.ImpulseEngineFuelMaxLevel;

    private double _fuelLevel;

    protected ImpulseEngine(double initialFuelLevel)
    {
        FuelLevel = initialFuelLevel;
    }

    public double CurrentVelocity { get; set; }

    public abstract double FuelConsumption { get; }

    public double FuelLevel
    {
        get => _fuelLevel;
        set
        {
            if (value < 0)
                throw new ArgumentException($"Fuel level value must be positive or 0, passed {value}");
            if (value > MaxFuelLevel)
                throw new ArgumentException($"Fuel level value must be less than max fuel level {MaxFuelLevel}");
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
}