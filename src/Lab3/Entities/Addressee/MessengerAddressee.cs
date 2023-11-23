using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.MessengerEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class MessengerAddressee : IAddressee
{
    private readonly IMessenger _messenger;

    public MessengerAddressee(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public int MinImportanceLevel => 2;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        _messenger.SendMessage(message);
    }

    public string GetPrefix()
    {
        return _messenger.Assign;
    }
}