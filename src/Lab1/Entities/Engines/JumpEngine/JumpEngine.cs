using System;
using Itmo.ObjectOrientedProgramming.Lab1.Config;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

public abstract class JumpEngine : IEngine
{
    private static readonly double MaxFuelLevel = EnginesConfig.JumpEngineFuelMaxLevel;

    private double _fuelLevel;

    protected JumpEngine(double initialFuelLevel)
    {
        FuelLevel = initialFuelLevel;
    }

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

    public double CurrentVelocity { get; set; }
    public abstract double FuelConsumption { get; }

    public double LaunchTime => 0.0;

    public double FillUpFuelLevel(double fillUpValue)
    {
        return FuelLevel += fillUpValue;
    }

    public abstract FlightResultResponse Fly(IEnvironment environment, double effectiveness);

    public abstract T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor);
}