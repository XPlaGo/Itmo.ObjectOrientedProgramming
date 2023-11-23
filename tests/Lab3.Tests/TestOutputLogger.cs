using System;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class TestOutputLogger : ILogger
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestOutputLogger(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public void LogOut(string logOut)
    {
        _testOutputHelper.WriteLine($"{DateTime.Now} Log: {logOut}");
    }

    public void WarningOut(string warningOut)
    {
        _testOutputHelper.WriteLine($"{DateTime.Now} Warning: {warningOut}");
    }

    public void ErrorOut(string errorOut)
    {
        _testOutputHelper.WriteLine($"{DateTime.Now} Error: {errorOut}");
    }
}