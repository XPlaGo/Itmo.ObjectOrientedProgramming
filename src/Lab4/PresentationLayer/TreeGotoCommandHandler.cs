using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.MemoryFilePath;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public class TreeGotoCommandHandler : ICommandHandler
{
    public TreeGotoCommandHandler(ICommandHandler? successor)
    {
        Successor = successor;
    }

    public ICommandHandler? Successor { get; set; }

    public string HandleCommand(string command, ICommandReceiver<string> receiver)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(receiver);

        if (command.StartsWith("goto ", StringComparison.Ordinal))
        {
            IList<string> args = command.Split(" ");
            if (args.Count != 2) throw new CommandFormatException(command);

            var gotoCommand = new GotoCommand(receiver, new Path(args[1]));
            return gotoCommand.Execute();
        }

        if (Successor is not null)
        {
            return Successor.HandleCommand(command, receiver);
        }

        throw new UnknownCommandException(command);
    }
}