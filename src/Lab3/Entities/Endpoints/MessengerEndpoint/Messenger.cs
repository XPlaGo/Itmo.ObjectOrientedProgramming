using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messenger.MessengerService;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.MessengerEndpoint;

public class Messenger : IMessenger
{
    private readonly IList<IMessage<IPrintable>> _messages = new List<IMessage<IPrintable>>();
    private readonly IMessengerService _messengerService;

    public Messenger(string name, string companyName, IMessengerService messengerService)
    {
        Name = name;
        CompanyName = companyName;
        _messengerService = messengerService;
    }

    public string Name { get; }
    public string CompanyName { get; }
    public string Assign => $"Messenger {Name}({CompanyName})";

    public void SendMessage(IMessage<IPrintable> message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _messages.Add(message);
        _messengerService.Send(Assign, message.Body.Content.ToString());
    }

    public IMessage<IPrintable> GetMessageByIndex(int index)
    {
        if (index >= _messages.Count)
            throw new MessageListIndexOutOfBoundsException(nameof(index));

        return _messages[index];
    }
}