using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message)
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ForbiddenException()
    {
    }
}