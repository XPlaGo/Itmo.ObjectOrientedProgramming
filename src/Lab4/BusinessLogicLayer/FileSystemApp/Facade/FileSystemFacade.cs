using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Context;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Facade;

public class FileSystemFacade<TCommandResponse> : IFileSystemFacade<TCommandResponse>
{
    public FileSystemFacade(IFileSystemContext context, IFileSystemService<TCommandResponse> service)
    {
        Context = context;
        Service = service;
    }

    public IFileSystemContext Context { get; }
    public IFileSystemService<TCommandResponse> Service { get; }

    public string Connect()
    {
        IDirectory directory = Context.ConnectionState.Connect();
        SetRootDirectory(directory);
        SetCurrentDirectory(directory);
        SetState(new ConnectedState<TCommandResponse>(Service));

        return "connected";
    }

    public string Disconnect()
    {
        Context.ConnectionState.Disconnect();
        SetState(new DisconnectedState<TCommandResponse>(Service));

        return "disconnected";
    }

    public void SetState(IConnectionState state)
    {
        Context.ConnectionState = state;
    }

    public void SetCurrentDirectory(IDirectory directory)
    {
        Context.CurrentDirectory = directory;
    }

    public void SetRootDirectory(IDirectory directory)
    {
        Context.RootDirectory = directory;
    }
}