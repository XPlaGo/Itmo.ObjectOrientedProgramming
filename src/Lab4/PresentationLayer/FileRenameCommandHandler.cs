using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command.File;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.MemoryFilePath;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public class FileRenameCommandHandler : ICommandHandler
{
    public FileRenameCommandHandler(ICommandHandler? successor)
    {
        Successor = successor;
    }

    public ICommandHandler? Successor { get; set; }
    public string HandleCommand(string command, ICommandReceiver<string> receiver)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(receiver);

        if (command.StartsWith("rename ", StringComparison.Ordinal))
        {
            IList<string> args = command.Split(" ");
            if (args.Count != 3) throw new CommandFormatException(command);

            IList<string> fileNewName = args[2].Split(".");
            if (fileNewName.Count != 2) throw new CommandFormatException(command);

            var copyCommand =
                new FileRenameCommand<string>(receiver, new Path(args[1]), fileNewName[0], fileNewName[1]);
            copyCommand.Execute();
        }

        if (Successor is not null)
        {
            return Successor.HandleCommand(command, receiver);
        }

        throw new UnknownCommandException(command);
    }
}