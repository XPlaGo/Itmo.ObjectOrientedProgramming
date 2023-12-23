using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

public class UnknownCommandException : ArgumentException
{
    public UnknownCommandException(string message)
        : base(message)
    {
    }

    public UnknownCommandException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public UnknownCommandException()
    {
    }
}