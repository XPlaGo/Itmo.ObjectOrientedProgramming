using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.MessengerEndpoint;

public interface IMessenger
{
    public string Name { get; }
    public string CompanyName { get; }
    public string Assign { get; }

    public void SendMessage(IMessage<IPrintable> message);
}