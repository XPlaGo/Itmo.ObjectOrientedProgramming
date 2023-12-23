namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Fabrics;

public class CommandHandlersFabrics : ICommandHandlersFabrics
{
    public ICommandHandler GetDefault()
    {
        return new ConnectCommandHandler(
            new DisconnectCommandHandler(
                new TreeCommandHandler(
                    new FileCommandHandler(
                        null,
                        new FileCopyCommandHandler(
                            new FileDeleteCommandHandler(
                                new FileDeleteCommandHandler(
                                    new FileMoveCommandHandler(
                                        new FileRenameCommandHandler(
                                            new FileShowCommandHandler(null))))))),
                    new TreeListCommandHandler(
                new TreeGotoCommandHandler(null)))));
    }
}