using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.MemoryFilePath;

public class Path : IPath
{
    public Path(string path)
    {
        Data = path;
    }

    public string Data { get; }

    public IList<string> ToParts()
    {
        return Data.Split("/");
    }

    public bool IsAbsolute()
    {
        return !Data.StartsWith("/", StringComparison.Ordinal);
    }
}