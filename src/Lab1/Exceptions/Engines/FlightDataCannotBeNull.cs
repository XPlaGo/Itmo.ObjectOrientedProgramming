using System;
using System.Data;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class FlightDataCannotBeNull : NoNullAllowedException
{
    public FlightDataCannotBeNull(string message)
        : base(message) { }

    public FlightDataCannotBeNull() { }

    public FlightDataCannotBeNull(string message, Exception innerException)
        : base(message, innerException) { }
}