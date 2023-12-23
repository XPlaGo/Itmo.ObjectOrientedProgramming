using System.Data;

namespace Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Exceptions;

public class RootDirectoryCannotBeNull : NoNullAllowedException
{
    public RootDirectoryCannotBeNull(string message)
        : base(message)
    {
    }

    public RootDirectoryCannotBeNull()
    {
    }

    public RootDirectoryCannotBeNull(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}