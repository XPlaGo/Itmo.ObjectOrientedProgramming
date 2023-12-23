using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.State;

public class AlreadyConnectedException : Exception
{
    public AlreadyConnectedException(string message)
        : base(message)
    {
    }

    public AlreadyConnectedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public AlreadyConnectedException()
    {
    }
}