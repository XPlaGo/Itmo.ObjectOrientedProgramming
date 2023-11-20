using System.IO;
using Itmo.ObjectOrientedProgramming.Lab2.Documents;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories.Cpu;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

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
        var repository = new ListCpuRepository();
        repository.InitFromJson(new FileInfo("Database/cpu.json"));
        CpuDocument? item = repository.GetById(new DocumentId("sdlcjsdlkcmsldkc"));
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(item));
    }
}