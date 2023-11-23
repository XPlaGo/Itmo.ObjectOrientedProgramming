using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Body;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Header;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.User;

public class MessageStatusDecorator<TMessage, TContent> : IMessage<TContent>
    where TContent : IPrintable
    where TMessage : IMessage<TContent>
{
    private readonly IMessage<TContent> _message;

    public MessageStatusDecorator(TMessage message)
    {
        _message = message;
    }

    public MessageStatus MessageStatus { get; set; } = MessageStatus.Unread;

    public IHeader Header => _message.Header;
    public IBody<TContent> Body => _message.Body;
    public int ImportanceLevel => _message.ImportanceLevel;
}