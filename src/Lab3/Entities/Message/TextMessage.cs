using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Body;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Header;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;

public class TextMessage : IMessage<Text>
{
    public TextMessage(
        string text,
        int importanceLevel)
    {
        Header = new TextHeader();
        Body = new Body(text);
        ImportanceLevel = importanceLevel;
    }

    public IHeader Header { get; }
    public IBody<Text> Body { get; }
    public int ImportanceLevel { get; }
}