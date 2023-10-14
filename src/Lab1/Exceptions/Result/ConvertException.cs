using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Result;

public class ConvertException : ArgumentOutOfRangeException
{
    public ConvertException(string message)
        : base(message) { }

    public ConvertException() { }

    public ConvertException(string message, Exception innerException)
        : base(message, innerException) { }
}