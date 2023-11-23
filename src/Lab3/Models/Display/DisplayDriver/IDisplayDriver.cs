namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Display.DisplayDriver;

public interface IDisplayDriver
{
    public Color Color { get; set; }

    public void Clear();

    public void Write(string data);
}