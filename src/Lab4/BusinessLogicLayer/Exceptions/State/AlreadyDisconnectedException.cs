using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.State;

public class AlreadyDisconnectedException : Exception
{
    public AlreadyDisconnectedException(string message)
        : base(message)
    {
    }

    public AlreadyDisconnectedException()
    {
    }

    public AlreadyDisconnectedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}