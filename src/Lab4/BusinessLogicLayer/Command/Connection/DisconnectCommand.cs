namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;

public class DisconnectCommand : IConnectionCommand<string>
{
    private readonly IConnectionCommandReceiver _receiver;

    public DisconnectCommand(IConnectionCommandReceiver receiver)
    {
        _receiver = receiver;
    }

    public string Execute()
    {
        return _receiver.Disconnect();
    }
}