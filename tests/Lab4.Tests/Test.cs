using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Command;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.File;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Context;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.FileSystemApp.Facade;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Element;
using Itmo.ObjectOrientedProgramming.Lab4.MemoryFileSystemLayer.Service;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer;
using Itmo.ObjectOrientedProgramming.Lab4.PresentationLayer.Fabrics;
using Xunit;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class Test
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var directory = new Directory(
            "root",
            new List<IFile>
            {
                new File("test1", "txt", "test data 1"),
                new File("test2", "txt", "test data java"),
            },
            new List<IDirectory>
            {
                new Directory(
                    "dir1",
                    new List<IFile>
                    {
                        new File("test3", "txt", "test data 3"),
                        new File("test4", "txt", "test data 4"),
                    },
                    new List<IDirectory>()),
                new Directory(
                    "dir2",
                    new List<IFile>
                    {
                        new File("test5", "txt", "test data 5"),
                        new File("test6", "txt", "test data 6"),
                    },
                    new List<IDirectory>()),
            });

        var service = new MemoryFileSystemService(directory);

        var facade = new FileSystemFacade<string>(
            new FileSystemContext(
                directory,
                directory,
                new DisconnectedState<string>(service)),
            service);

        var receiver = new CommandReceiver<string>(facade);

        ICommandHandler commandHandler = new CommandHandlersFabrics().GetDefault();

        _testOutputHelper.WriteLine(commandHandler.HandleCommand("tree list 2", receiver));
    }
}