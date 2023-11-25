using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Topic;

public class DefaultTopic : ITopic
{
    public DefaultTopic(
        string name,
        IAddressee addressee)
    {
        Name = name;
        Addressee = addressee;
    }

    public string Name { get; }
    public IAddressee Addressee { get; }
    public void SendMessage<TMessage>(TMessage message)
        where TMessage : IMessage<IPrintable>
    {
        Addressee.HandleMessage(message);
    }
}