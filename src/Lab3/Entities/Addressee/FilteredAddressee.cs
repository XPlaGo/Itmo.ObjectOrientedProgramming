using System;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class FilteredAddressee<TAddressee> : IAddressee
    where TAddressee : IAddressee
{
    private readonly TAddressee _addressee;

    public FilteredAddressee(TAddressee addressee)
    {
        _addressee = addressee;
    }

    public int MinImportanceLevel => _addressee.MinImportanceLevel;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (CanCall(message)) _addressee.HandleMessage(message);
    }

    public string GetPrefix()
    {
        return _addressee.GetPrefix();
    }

    private bool CanCall(IMessage<IPrintable> message)
    {
        return MinImportanceLevel <= message.ImportanceLevel;
    }
}