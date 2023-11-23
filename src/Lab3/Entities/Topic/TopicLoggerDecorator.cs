using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Topic;

public class TopicLoggerDecorator : ITopic
{
    private readonly ITopic _topic;
    private readonly ILogger _logger;

    public TopicLoggerDecorator(ITopic topic, ILogger logger)
    {
        _topic = topic;
        _logger = logger;
    }

    public string Name => _topic.Name;
    public IAddressee Addressee => _topic.Addressee;
    public void SendMessage<TMessage>(TMessage message)
        where TMessage : IMessage<IPrintable>
    {
        _logger.LogOut($"Send message with {Name} topic: {message.Body.Content.ToString()}");
        _topic.SendMessage(message);
    }
}