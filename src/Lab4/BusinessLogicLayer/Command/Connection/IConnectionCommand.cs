namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;

public interface IConnectionCommand<out TResponse>
{
    public TResponse Execute();
}