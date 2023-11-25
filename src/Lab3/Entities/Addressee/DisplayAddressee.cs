using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.DisplayEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class DisplayAddressee : IAddressee
{
    private readonly IDisplay _display;

    public DisplayAddressee(IDisplay display)
    {
        _display = display;
    }

    public int MinImportanceLevel => 1;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        _display.ShowMessage(message);
    }

    public string GetPrefix()
    {
        return _display.Description;
    }
}