using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Body;

public class Body : IBody<Text>
{
    public Body(Text text)
    {
        Content = text;
    }

    public Body(string text)
    {
        Content = new Text(text);
    }

    public Text Content { get; }
}