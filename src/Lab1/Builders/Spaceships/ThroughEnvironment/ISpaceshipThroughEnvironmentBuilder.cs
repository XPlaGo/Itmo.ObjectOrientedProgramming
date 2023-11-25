using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.ThroughEnvironment;

public interface ISpaceshipThroughEnvironmentBuilder<TResult, out TSpaceship>
{
    ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughSpace(Func<TSpaceship, Space, TResult> fly);
    ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughNebulaeOfIncreasedDensityOfSpace(Func<TSpaceship, NebulaeOfIncreasedDensityOfSpace, TResult> fly);
    ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughNebulaeOfNitrideParticles(Func<TSpaceship, NebulaeOfNitrideParticles, TResult> fly);
    SpaceshipThroughEnvironmentDispatcher<TResult> Confirm();
}