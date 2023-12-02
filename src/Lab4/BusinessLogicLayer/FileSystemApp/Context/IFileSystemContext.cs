using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Context;

public interface IFileSystemContext
{
    public IDirectory CurrentDirectory { get; set; }
    public IDirectory RootDirectory { get; set; }
    public IConnectionState ConnectionState { get; set; }
}