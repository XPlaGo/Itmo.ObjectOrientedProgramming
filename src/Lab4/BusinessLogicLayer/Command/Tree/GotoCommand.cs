using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;

public class GotoCommand : ITreeCommand<string>
{
    private readonly ITreeReceiver _receiver;
    private readonly IPath _path;

    public GotoCommand(ITreeReceiver receiver, IPath path)
    {
        _receiver = receiver;
        _path = path;
    }

    public string Execute()
    {
        return _receiver.GoToPath(_path);
    }
}