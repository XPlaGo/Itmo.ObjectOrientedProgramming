using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Facade;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command;

public class CommandReceiver<TCommandResponse> : ICommandReceiver<TCommandResponse>
{
    private readonly IFileSystemFacade<TCommandResponse> _facade;

    public CommandReceiver(
        IFileSystemFacade<TCommandResponse> facade)
    {
        _facade = facade;
    }

    public TCommandResponse Copy(IPath fromPath, IPath toPath)
    {
        return _facade.Service.Copy(fromPath, toPath, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
    }

    public TCommandResponse Delete(IPath path)
    {
        return _facade.Service.Delete(path, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
    }

    public TCommandResponse Move(IPath fromPath, IPath toPath)
    {
        return _facade.Service.Move(fromPath, toPath, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
    }

    public TCommandResponse Rename(IPath path, string newName, string newFormat)
    {
        return _facade.Service.Rename(path, newName, newFormat, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
    }

    public TCommandResponse Show(IPath path)
    {
        return _facade.Service.Show(path, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
    }

    public string GoToPath(IPath path)
    {
        IDirectory directory =
            _facade.Service.GoToPath(path, _facade.Context.CurrentDirectory, _facade.Context.RootDirectory);
        _facade.SetCurrentDirectory(directory);
        return directory.Name;
    }

    public string List(int depth)
    {
        return _facade.Service.List(depth, _facade.Context.CurrentDirectory);
    }

    public string Connect()
    {
        return _facade.Connect();
    }

    public string Disconnect()
    {
        return _facade.Disconnect();
    }
}