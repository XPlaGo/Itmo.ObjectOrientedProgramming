using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.User;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.UserEndpoint;

public class User : IUser
{
    private readonly IList<MessageStatusDecorator<IMessage<IPrintable>, IPrintable>> _messages = new List<MessageStatusDecorator<IMessage<IPrintable>, IPrintable>>();

    public User(string username)
    {
        Username = username;
    }

    public string Username { get; }

    public string Prefix => $"User {Username}";

    public void HandleMessage(IMessage<IPrintable> message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _messages.Add(new MessageStatusDecorator<IMessage<IPrintable>, IPrintable>(message));
    }

    public void ReadMessageByIndex(int index)
    {
        if (index >= _messages.Count)
            throw new MessageListIndexOutOfBoundsException(nameof(index));

        if (_messages[index].MessageStatus == MessageStatus.Read)
            throw new MessageAlreadyReadException(_messages[index].GetType().Name);

        _messages[index].MessageStatus = MessageStatus.Read;
    }

    public MessageStatus GetMessageStatusByIndex(int index)
    {
        if (index >= _messages.Count)
            throw new MessageListIndexOutOfBoundsException(nameof(index));

        return _messages[index].MessageStatus;
    }
}