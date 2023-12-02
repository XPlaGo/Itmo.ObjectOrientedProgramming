using System;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public class FileCommandHandler : ICommandHandler
{
    private readonly ICommandHandler _heritor;

    public FileCommandHandler(ICommandHandler? successor, ICommandHandler heritor)
    {
        Successor = successor;
        _heritor = heritor;
    }

    public ICommandHandler? Successor { get; set; }

    public string HandleCommand(string command, ICommandReceiver<string> receiver)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(receiver);

        if (command.StartsWith("file ", StringComparison.Ordinal))
        {
            return _heritor.HandleCommand(command[5..], receiver);
        }

        if (Successor is not null)
        {
            return Successor.HandleCommand(command, receiver);
        }

        throw new UnknownCommandException(command);
    }
}