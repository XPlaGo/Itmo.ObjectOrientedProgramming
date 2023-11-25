using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class FuelException : ArgumentException
{
    public FuelException(double level, double min, double max)
        : base($"Fuel level must be between {min} and {max}, passed {level}") { }

    public FuelException() { }

    public FuelException(string message, Exception innerException)
        : base(message, innerException) { }

    public FuelException(string message)
        : base(message) { }
}