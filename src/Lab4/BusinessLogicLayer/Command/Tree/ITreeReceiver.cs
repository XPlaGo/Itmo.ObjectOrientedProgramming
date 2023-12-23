using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Path;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;

public interface ITreeReceiver
{
    public string GoToPath(IPath path);
    public string List(int depth);
}