using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.MemoryFilePath;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public class FileDeleteCommandHandler : ICommandHandler
{
    public FileDeleteCommandHandler(ICommandHandler? successor)
    {
        Successor = successor;
    }

    public ICommandHandler? Successor { get; set; }
    public string HandleCommand(string command, ICommandReceiver<string> receiver)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(receiver);

        if (command.StartsWith("delete ", StringComparison.Ordinal))
        {
            IList<string> args = command.Split(" ");
            if (args.Count != 2) throw new CommandFormatException(command);

            var deleteCommand =
                new FileDeleteCommand<string>(receiver, new Path(args[1]));
            return deleteCommand.Execute();
        }

        if (Successor is not null)
        {
            return Successor.HandleCommand(command, receiver);
        }

        throw new UnknownCommandException(command);
    }
}