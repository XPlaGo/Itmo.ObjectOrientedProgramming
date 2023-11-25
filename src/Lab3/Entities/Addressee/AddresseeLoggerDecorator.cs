using System;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class AddresseeLoggerDecorator<TAddressee> : IAddressee
    where TAddressee : IAddressee
{
    private readonly TAddressee _addressee;
    private readonly ILogger _logger;

    public AddresseeLoggerDecorator(TAddressee addressee, ILogger logger)
    {
        _addressee = addressee;
        _logger = logger;
    }

    public int MinImportanceLevel => _addressee.MinImportanceLevel;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _logger.LogOut($"{_addressee.GetPrefix()} handles message: " + message.Body.Content.ToString());
        _addressee.HandleMessage(message);
    }

    public string GetPrefix()
    {
        return _addressee.GetPrefix();
    }
}