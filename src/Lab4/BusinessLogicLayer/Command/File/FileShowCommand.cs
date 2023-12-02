using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;

public class FileShowCommand<TResponse> : IFileCommand<TResponse>
{
    private readonly IFileCommandReceiver<TResponse> _receiver;
    private readonly IPath _path;

    public FileShowCommand(IFileCommandReceiver<TResponse> receiver, IPath path)
    {
        _receiver = receiver;
        _path = path;
    }

    public TResponse Execute()
    {
        return _receiver.Show(_path);
    }
}