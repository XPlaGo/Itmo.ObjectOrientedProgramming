using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class FuelException : ArgumentException
{
    public FuelException(string message)
        : base(message) { }

    public FuelException() { }

    public FuelException(string message, Exception innerException)
        : base(message, innerException) { }
}