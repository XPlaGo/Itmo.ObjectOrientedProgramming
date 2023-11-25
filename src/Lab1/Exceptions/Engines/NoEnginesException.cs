using System;
using System.Data;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;

public class NoEnginesException : NoNullAllowedException
{
    public NoEnginesException(string message)
        : base(message) { }

    public NoEnginesException() { }

    public NoEnginesException(string message, Exception innerException)
        : base(message, innerException) { }
}