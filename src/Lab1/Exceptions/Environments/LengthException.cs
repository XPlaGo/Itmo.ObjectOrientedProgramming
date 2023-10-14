using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Environments;

public class LengthException : ArgumentException
{
    public LengthException(string message)
        : base(message) { }

    public LengthException() { }

    public LengthException(string message, Exception innerException)
        : base(message, innerException) { }
}