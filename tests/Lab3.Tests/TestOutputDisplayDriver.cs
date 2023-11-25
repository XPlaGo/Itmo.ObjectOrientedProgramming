using Itmo.ObjectOrientedProgramming.Lab3.Models.Display.DisplayDriver;
using Xunit.Abstractions;
using static Crayon.Output;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class TestOutputDisplayDriver : IDisplayDriver
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestOutputDisplayDriver(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public Color Color { get; set; } = Color.Red;
    public void Clear()
    {
        // Console.Clear();
    }

    public void Write(string data)
    {
        switch (Color)
        {
            case Color.Black:
                _testOutputHelper.WriteLine(Black(data));
                break;
            case Color.Red:
                _testOutputHelper.WriteLine(Red(data));
                break;
            case Color.Green:
                _testOutputHelper.WriteLine(Green(data));
                break;
            case Color.Blue:
                _testOutputHelper.WriteLine(Blue(data));
                break;
            case Color.White:
                _testOutputHelper.WriteLine(White(data));
                break;
            default:
                _testOutputHelper.WriteLine(Black(data));
                break;
        }
    }
}