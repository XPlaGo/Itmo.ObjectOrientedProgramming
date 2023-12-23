namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;

public interface ITreeCommand<out TResponse>
{
    public TResponse Execute();
}