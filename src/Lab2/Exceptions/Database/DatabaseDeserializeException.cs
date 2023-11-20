using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Database;

public class DatabaseDeserializeException : Exception
{
    public DatabaseDeserializeException(string message)
        : base(message)
    {
    }

    public DatabaseDeserializeException()
    {
    }

    public DatabaseDeserializeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}