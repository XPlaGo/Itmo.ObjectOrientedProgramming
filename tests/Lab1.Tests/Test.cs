using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Path;
using Xunit;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class Test
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PathService _pathService = new();

    public Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfIncreasedDensityOfSpace(
                1000,
                new List<IImpediment>()));

        Path path = pathBuilder.Build();
        var pathClone = (Path)path.Clone();

        var walkingSpaceship = new WalkingSpaceship(5, 10);
        var augurSpaceship = new AugurSpaceship(5, 5);
        Assert.Equal(SpaceshipResult.NotEnoughFuel, _pathService.Fly(walkingSpaceship, path).SpaceshipResult);
        Assert.Equal(SpaceshipResult.NotEnoughFuel, _pathService.Fly(augurSpaceship, pathClone).SpaceshipResult);
    }

    [Fact]
    public void Test2()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfIncreasedDensityOfSpace(
                1000,
                new List<IImpediment>
                {
                    new AntimatterFlares(100),
                }));

        Path path = pathBuilder.Build();
        var pathClone = (Path)path.Clone();

        var vaclasSpaceship1 = new VaclasSpaceship(5, 10);
        var vaclasSpaceship2 = new VaclasSpaceship(5, 10, true, true);
        Assert.Equal(SpaceshipResult.LossOfCrew, _pathService.Fly(vaclasSpaceship1, path).SpaceshipResult);
        Assert.Equal(SpaceshipResult.Overcome, _pathService.Fly(vaclasSpaceship2, pathClone).SpaceshipResult);
    }

    [Fact]
    public void Test3()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfNitrideParticles(
                2,
                new List<IImpediment>
                {
                    new CosmoWhales(100),
                }));

        Path path = pathBuilder.Build();
        var pathClone1 = (Path)path.Clone();
        var pathClone2 = (Path)path.Clone();

        var vaclasSpaceship = new VaclasSpaceship(5, 10, false);
        var augurSpaceship = new AugurSpaceship(5, 10);
        var meridianSpaceship = new MeridianSpaceship(5);

        Assert.Equal(SpaceshipResult.Destroyed, _pathService.Fly(vaclasSpaceship, path).SpaceshipResult);

        Assert.Equal(SpaceshipResult.Overcome, _pathService.Fly(augurSpaceship, pathClone1).SpaceshipResult);
        Assert.Equal(0, augurSpaceship.Deflector.DeflectorPoints);

        Assert.Equal(SpaceshipResult.Overcome, _pathService.Fly(meridianSpaceship, pathClone2).SpaceshipResult);
        Assert.NotEqual(0, meridianSpaceship.Deflector.DeflectorPoints);
    }

    [Fact]
    public void Test4()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new Space(5, new List<IImpediment>()));

        Path path = pathBuilder.Build();

        var walkingSpaceship = new WalkingSpaceship(5, 10);
        var vaclasSpaceship = new VaclasSpaceship(5, 10);

        Assert.Equal(0, _pathService.GetOptimal(new List<ISpaceship> { walkingSpaceship, vaclasSpaceship }, path).Index);
    }

    [Fact]
    public void Test5()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new NebulaeOfIncreasedDensityOfSpace(15, new List<IImpediment>()));

        Path path = pathBuilder.Build();

        var augurSpaceship = new AugurSpaceship(15, 10);
        var stellaSpaceship = new StellaSpaceship(15, 10);

        Assert.Equal(1, _pathService.GetOptimal(new List<ISpaceship> { augurSpaceship, stellaSpaceship }, path).Index);
    }

    [Fact]
    public void Test6()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new NebulaeOfNitrideParticles(5, new List<IImpediment>()));

        Path path = pathBuilder.Build();

        var walkingSpaceship = new WalkingSpaceship(5, 10);
        var vaclasSpaceship = new VaclasSpaceship(15, 10);

        Assert.Equal(1, _pathService.GetOptimal(new List<ISpaceship> { walkingSpaceship, vaclasSpaceship }, path).Index);
    }

    [Fact]
    public void Test7()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfNitrideParticles(
                1,
                new List<IImpediment>
                {
                    new CosmoWhales(100),
                }));
        pathBuilder.AddEnvironment(
            new Space(
                1,
                new List<IImpediment>
                {
                    new AntimatterFlares(100),
                }));
        pathBuilder.AddEnvironment(
            new NebulaeOfIncreasedDensityOfSpace(
                1,
                new List<IImpediment>
                {
                    new Meteorites(100, 10),
                }));

        Path path = pathBuilder.Build();

        var spaceship = new QuantumSpaceship(5, 10);

        Assert.Equal(SpaceshipResult.Overcome, _pathService.Fly(spaceship, path).SpaceshipResult);
    }
}