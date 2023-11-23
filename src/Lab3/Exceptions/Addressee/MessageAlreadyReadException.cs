using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;

public class MessageAlreadyReadException : Exception
{
    public MessageAlreadyReadException(string message)
        : base(message)
    {
    }

    public MessageAlreadyReadException()
    {
    }

    public MessageAlreadyReadException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}