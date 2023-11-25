using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class GroupAddressee : IAddressee
{
    private readonly IReadOnlyList<IAddressee> _addressees;

    public GroupAddressee(IReadOnlyList<IAddressee> addressees)
    {
        _addressees = addressees;
    }

    public int MinImportanceLevel => 0;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        foreach (IAddressee addressee in _addressees)
        {
            addressee.HandleMessage(message);
        }
    }

    public string GetPrefix()
    {
        return "Group of addresses";
    }
}