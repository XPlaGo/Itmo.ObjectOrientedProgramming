namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;

public class ListCommand : ITreeCommand<string>
{
    private readonly ITreeReceiver _receiver;
    private readonly int _depth;

    public ListCommand(ITreeReceiver receiver, int depth)
    {
        _receiver = receiver;
        _depth = depth;
    }

    public string Execute()
    {
        return _receiver.List(_depth);
    }
}