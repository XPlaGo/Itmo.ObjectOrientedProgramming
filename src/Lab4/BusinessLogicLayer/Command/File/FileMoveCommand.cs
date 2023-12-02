using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;

public class FileMoveCommand<TResponse> : IFileCommand<TResponse>
{
    private readonly IFileCommandReceiver<TResponse> _receiver;
    private readonly IPath _fromPath;
    private readonly IPath _toPath;

    public FileMoveCommand(IFileCommandReceiver<TResponse> receiver, IPath fromPath, IPath toPath)
    {
        _receiver = receiver;
        _fromPath = fromPath;
        _toPath = toPath;
    }

    public TResponse Execute()
    {
        return _receiver.Move(_fromPath, _toPath);
    }
}