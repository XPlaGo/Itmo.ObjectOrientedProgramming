using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

public interface IPath
{
    public string Data { get; }
    public IList<string> ToParts();
    public bool IsAbsolute();
}