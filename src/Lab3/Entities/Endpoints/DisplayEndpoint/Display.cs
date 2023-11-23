using System;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Display.DisplayDriver;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.DisplayEndpoint;

public class Display : IDisplay
{
    private readonly IDisplayDriver _displayDriver;

    public Display(string name, IDisplayDriver displayDriver)
    {
        _displayDriver = displayDriver;
        Name = name;
    }

    public Color Color
    {
        get => _displayDriver.Color;
        set => _displayDriver.Color = value;
    }

    public string Name { get; }
    public string Description => $"Display {Name}";

    public void ShowMessage(IMessage<IPrintable> message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _displayDriver.Clear();
        _displayDriver.Write(message.Body.Content.ToString());
    }
}