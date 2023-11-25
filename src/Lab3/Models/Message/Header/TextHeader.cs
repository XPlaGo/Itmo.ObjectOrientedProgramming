namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Header;

public class TextHeader : IHeader
{
    public string MessageFormat { get; set; } = "text";
}