using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public interface IAddressee
{
    public int MinImportanceLevel { get; }
    public void HandleMessage(IMessage<IPrintable> message);
    public string GetPrefix();
}