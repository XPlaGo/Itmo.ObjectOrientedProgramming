using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;

public class MessageListIndexOutOfBoundsException : ArgumentOutOfRangeException
{
    public MessageListIndexOutOfBoundsException(string message)
        : base(message)
    {
    }

    public MessageListIndexOutOfBoundsException()
    {
    }

    public MessageListIndexOutOfBoundsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}