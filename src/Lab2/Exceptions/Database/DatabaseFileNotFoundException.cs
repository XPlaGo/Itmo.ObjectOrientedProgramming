using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Database;

public class DatabaseFileNotFoundException : FileNotFoundException
{
    public DatabaseFileNotFoundException(string message)
        : base(message)
    {
    }

    public DatabaseFileNotFoundException()
    {
    }

    public DatabaseFileNotFoundException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}