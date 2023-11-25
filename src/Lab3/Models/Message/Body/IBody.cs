using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Body;

public interface IBody<out TContent>
    where TContent : IPrintable
{
    public TContent Content { get; }
}