using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

public class CommandFormatException : ArgumentException
{
    public CommandFormatException(string message)
        : base(message)
    {
    }

    public CommandFormatException()
    {
    }

    public CommandFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}