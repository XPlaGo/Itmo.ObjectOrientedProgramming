using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;

public class FileRenameCommand<TResponse> : IFileCommand<TResponse>
{
    private readonly IFileCommandReceiver<TResponse> _receiver;
    private readonly IPath _path;
    private readonly string _newName;
    private readonly string _newFormat;

    public FileRenameCommand(IFileCommandReceiver<TResponse> receiver, IPath path, string newName, string newFormat)
    {
        _receiver = receiver;
        _path = path;
        _newName = newName;
        _newFormat = newFormat;
    }

    public TResponse Execute()
    {
        return _receiver.Rename(_path, _newName, _newFormat);
    }
}