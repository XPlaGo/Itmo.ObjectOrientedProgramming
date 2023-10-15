using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Path;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class Test
{
    private readonly PathService _pathService = new();

    public static IEnumerable<object[]> EngineTestData()
    {
        yield return new object[] { new WalkingSpaceship(5, 10), SpaceshipResult.NotEnoughFuel };
        yield return new object[] { new AugurSpaceship(5, 5), SpaceshipResult.NotEnoughFuel };
    }

    public static IEnumerable<object[]> AntimatterFlareTestData()
    {
        yield return new object[] { new VaclasSpaceship(5, 10), SpaceshipResult.LossOfCrew };
        yield return new object[] { new VaclasSpaceship(5, 10, true, true), SpaceshipResult.Overcome };
    }

    public static IEnumerable<object[]> CosmoWhaleTestData()
    {
        yield return new object[] { new VaclasSpaceship(5, 10, false), SpaceshipResult.Destroyed };
        yield return new object[] { new AugurSpaceship(5, 10), SpaceshipResult.Overcome };
        yield return new object[] { new MeridianSpaceship(5), SpaceshipResult.Overcome };
    }

    [Theory]
    [MemberData(nameof(EngineTestData))]
    public void EngineTest(ISpaceship spaceship, SpaceshipResult spaceshipResult)
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfIncreasedDensityOfSpace(
                1000,
                new List<IImpediment>()));

        SequentialPath sequentialPath = pathBuilder.Build();

        Assert.Equal(spaceshipResult, _pathService.Fly(spaceship, sequentialPath).SpaceshipResult);
    }

    [Theory]
    [MemberData(nameof(AntimatterFlareTestData))]
    public void AntimatterFlareTest(ISpaceship spaceship, SpaceshipResult spaceshipResult)
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfIncreasedDensityOfSpace(
                1000,
                new List<IImpediment>
                {
                    new AntimatterFlares(100),
                }));

        SequentialPath sequentialPath = pathBuilder.Build();

        Assert.Equal(spaceshipResult, _pathService.Fly(spaceship, sequentialPath).SpaceshipResult);
    }

    [Theory]
    [MemberData(nameof(CosmoWhaleTestData))]
    public void CosmoWhaleTest(ISpaceship spaceship, SpaceshipResult spaceshipResult)
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(
            new NebulaeOfNitrideParticles(
                2,
                new List<IImpediment>
                {
                    new CosmoWhales(100),
                }));

        SequentialPath sequentialPath = pathBuilder.Build();

        Assert.Equal(spaceshipResult, _pathService.Fly(spaceship, sequentialPath).SpaceshipResult);
    }

    [Fact]
    public void EnginesConsumptionTest()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new Space(5, new List<IImpediment>()));

        SequentialPath sequentialPath = pathBuilder.Build();

        var walkingSpaceship = new WalkingSpaceship(5, 10);
        var vaclasSpaceship = new VaclasSpaceship(5, 10);

        Assert.Equal(0, _pathService.GetOptimal(new List<ISpaceship> { walkingSpaceship, vaclasSpaceship }, sequentialPath).Index);
    }

    [Fact]
    public void LengthOptimalTest()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new NebulaeOfIncreasedDensityOfSpace(15, new List<IImpediment>()));

        SequentialPath sequentialPath = pathBuilder.Build();

        var augurSpaceship = new AugurSpaceship(15, 10);
        var stellaSpaceship = new StellaSpaceship(15, 10);

        Assert.Equal(1, _pathService.GetOptimal(new List<ISpaceship> { augurSpaceship, stellaSpaceship }, sequentialPath).Index);
    }

    [Fact]
    public void PathCompletedOptimalTest()
    {
        var pathBuilder = new PathBuilder();
        pathBuilder.AddEnvironment(new NebulaeOfNitrideParticles(5, new List<IImpediment>()));

        SequentialPath sequentialPath = pathBuilder.Build();

        var walkingSpaceship = new WalkingSpaceship(5, 10);
        var vaclasSpaceship = new VaclasSpaceship(15, 10);

        Assert.Equal(1, _pathService.GetOptimal(new List<ISpaceship> { walkingSpaceship, vaclasSpaceship }, sequentialPath).Index);
    }

    [Fact]
    public void MySpaceshipTest()
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

        SequentialPath sequentialPath = pathBuilder.Build();

        var spaceship = new QuantumSpaceship(5, 10);

        Assert.Equal(SpaceshipResult.Overcome, _pathService.Fly(spaceship, sequentialPath).SpaceshipResult);
    }
}