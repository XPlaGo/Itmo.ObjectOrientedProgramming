using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Body;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Header;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;

public interface IMessage<out TContent>
    where TContent : IPrintable
{
    public IHeader Header { get; }
    public IBody<TContent> Body { get; }
    public int ImportanceLevel { get; }
}