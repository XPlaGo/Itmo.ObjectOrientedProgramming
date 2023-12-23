namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;

public class ConnectCommand : IConnectionCommand<string>
{
    private readonly IConnectionCommandReceiver _receiver;

    public ConnectCommand(IConnectionCommandReceiver receiver)
    {
        _receiver = receiver;
    }

    public string Execute()
    {
        return _receiver.Connect();
    }
}