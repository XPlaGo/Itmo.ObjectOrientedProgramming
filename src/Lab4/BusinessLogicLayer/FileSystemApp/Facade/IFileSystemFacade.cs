using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Context;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Facade;

public interface IFileSystemFacade<out TCommandResponse>
{
    public IFileSystemContext Context { get; }
    public IFileSystemService<TCommandResponse> Service { get; }

    public string Connect();
    public string Disconnect();

    public void SetState(IConnectionState state);

    public void SetCurrentDirectory(IDirectory directory);

    public void SetRootDirectory(IDirectory directory);
}