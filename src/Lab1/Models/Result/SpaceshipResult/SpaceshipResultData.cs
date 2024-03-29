﻿using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

public class SpaceshipResultData
{
    public SpaceshipResultData(double time, double currentVelocity, double length, double fuel)
    {
        DoubleValidationException.ThrowIfLessThan(time, 0);
        DoubleValidationException.ThrowIfLessThan(currentVelocity, 0);
        DoubleValidationException.ThrowIfLessThan(length, 0);
        DoubleValidationException.ThrowIfLessThan(fuel, 0);

        Time = time;
        CurrentVelocity = currentVelocity;
        Length = length;
        Fuel = fuel;
    }

    public SpaceshipResultData(FlightResultData flightResultData)
    {
        ArgumentNullException.ThrowIfNull(flightResultData);

        Time = flightResultData.Time;
        CurrentVelocity = flightResultData.CurrentVelocity;
        Length = flightResultData.Length;
        Fuel = flightResultData.Fuel;
    }

    public double Time { get; set; }
    public double CurrentVelocity { get; set; }
    public double Length { get; set; }
    public double Fuel { get; set; }

    public static SpaceshipResultData Empty()
    {
        return new SpaceshipResultData(0, 0, 0, 0);
    }
}