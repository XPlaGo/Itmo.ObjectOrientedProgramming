using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class FuelRanOutException : Exception
{
    public FuelRanOutException(string engineName)
        : base($"The fuel ran out in the {engineName}") { }

    public FuelRanOutException() { }

    public FuelRanOutException(string message, Exception innerException)
        : base(message, innerException) { }
}