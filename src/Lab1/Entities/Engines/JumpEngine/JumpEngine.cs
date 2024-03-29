﻿using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

public abstract class JumpEngine : IEngine
{
    private const double JumpEngineFuelMaxLevel = 50;

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
            if (value is < 0 or > JumpEngineFuelMaxLevel)
                throw new FuelException(value, 0, JumpEngineFuelMaxLevel);
            _fuelLevel = value;
        }
    }

    public double CurrentVelocity { get; private set; }
    public abstract double FuelConsumption { get; }

    public double LaunchTime => 0.0;

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