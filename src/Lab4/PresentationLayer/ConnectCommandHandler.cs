using System;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Connection;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public class ConnectCommandHandler : ICommandHandler
{
    public ConnectCommandHandler(ICommandHandler? successor)
    {
        Successor = successor;
    }

    public ICommandHandler? Successor { get; set; }

    public string HandleCommand(string command, ICommandReceiver<string> receiver)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(receiver);

        if (command.StartsWith("connect ", StringComparison.Ordinal))
        {
            var connectCommand = new ConnectCommand(receiver);
            return connectCommand.Execute();
        }

        if (Successor is not null)
        {
            return Successor.HandleCommand(command, receiver);
        }

        throw new UnknownCommandException(command);
    }
}