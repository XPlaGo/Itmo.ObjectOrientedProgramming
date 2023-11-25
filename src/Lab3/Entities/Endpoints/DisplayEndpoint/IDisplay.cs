using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.DisplayEndpoint;

public interface IDisplay
{
    public string Name { get; }
    public string Description { get; }

    public void ShowMessage(IMessage<IPrintable> message);
}