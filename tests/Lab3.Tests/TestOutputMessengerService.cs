using Itmo.ObjectOrientedProgramming.Lab3.Models.Messenger.MessengerService;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class TestOutputMessengerService : IMessengerService
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestOutputMessengerService(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public void Send(string messengerAssign, string data)
    {
        _testOutputHelper.WriteLine($"{messengerAssign}: {data}");
    }
}