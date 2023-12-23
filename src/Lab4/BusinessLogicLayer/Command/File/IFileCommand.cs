namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;

public interface IFileCommand<out TResponse>
{
    public TResponse Execute();
}