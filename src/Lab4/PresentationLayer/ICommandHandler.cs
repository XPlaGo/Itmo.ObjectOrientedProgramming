using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer;

namespace Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;

public interface ICommandHandler
{
    public ICommandHandler? Successor { get; set; }
    public string HandleCommand(string command, ICommandReceiver<string> receiver);
}