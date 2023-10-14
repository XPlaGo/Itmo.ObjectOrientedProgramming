using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class NotEnoughFuelException : Exception
{
    public NotEnoughFuelException(string message)
        : base(message) { }

    public NotEnoughFuelException(double needFuelLevel, double currentFuelLevel, string engineName)
        : base($"Need {needFuelLevel} fuel for {engineName}, but there are {currentFuelLevel}") { }

    public NotEnoughFuelException() { }

    public NotEnoughFuelException(string message, Exception innerException)
        : base(message, innerException) { }
}