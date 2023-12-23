namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;

public interface IConnectionCommandReceiver
{
    public string Connect();
    public string Disconnect();
}