namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

public class Text : IPrintable
{
    private readonly string _text;

    public Text(string text)
    {
        _text = text;
    }

    public override string ToString()
    {
        return "Text { \"" + _text + "\" }";
    }
}