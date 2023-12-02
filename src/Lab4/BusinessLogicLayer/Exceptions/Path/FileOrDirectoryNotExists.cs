using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.Path;

public class FileOrDirectoryNotExists : FileNotFoundException
{
    public FileOrDirectoryNotExists(string message)
        : base(message)
    {
    }

    public FileOrDirectoryNotExists()
    {
    }

    public FileOrDirectoryNotExists(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}