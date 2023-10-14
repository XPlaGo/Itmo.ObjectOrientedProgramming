using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Components;

public class CannotUseNoneComponentException : Exception
{
    public CannotUseNoneComponentException(string message)
        : base(message) { }

    public CannotUseNoneComponentException() { }

    public CannotUseNoneComponentException(string message, Exception innerException)
        : base(message, innerException) { }
}