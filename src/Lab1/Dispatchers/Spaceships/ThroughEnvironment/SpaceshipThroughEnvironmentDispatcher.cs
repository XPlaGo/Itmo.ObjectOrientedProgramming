using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;

public class SpaceshipThroughEnvironmentDispatcher<TResult> : ISpaceshipThroughEnvironmentVisitor<IEnvironmentVisitor<TResult>>
{
    public SpaceshipThroughEnvironmentDispatcher(Func<ISpaceship, IEnvironment, TResult> fly)
    {
        ByWalkingSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, WalkingSpaceship>(this, fly);
        ByVaclasSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, VaclasSpaceship>(this, fly);
        ByMeridianSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, MeridianSpaceship>(this, fly);
        ByStellaSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, StellaSpaceship>(this, fly);
        ByAugurSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, AugurSpaceship>(this, fly);
        ByQuantumSpaceship = new SpaceshipThroughEnvironmentBuilder<TResult, QuantumSpaceship>(this, fly);
    }

    public SpaceshipThroughEnvironmentBuilder<TResult, WalkingSpaceship> ByWalkingSpaceship { get; }
    public SpaceshipThroughEnvironmentBuilder<TResult, VaclasSpaceship> ByVaclasSpaceship { get; }
    public SpaceshipThroughEnvironmentBuilder<TResult, MeridianSpaceship> ByMeridianSpaceship { get; }
    public SpaceshipThroughEnvironmentBuilder<TResult, StellaSpaceship> ByStellaSpaceship { get; }
    public SpaceshipThroughEnvironmentBuilder<TResult, AugurSpaceship> ByAugurSpaceship { get; }
    public SpaceshipThroughEnvironmentBuilder<TResult, QuantumSpaceship> ByQuantumSpaceship { get; }

    public IEnvironmentVisitor<TResult> Visit(WalkingSpaceship spaceship)
    {
        return ByWalkingSpaceship.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(VaclasSpaceship spaceship)
    {
        return ByVaclasSpaceship.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(MeridianSpaceship spaceship)
    {
        return ByMeridianSpaceship.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(StellaSpaceship spaceship)
    {
        return ByStellaSpaceship.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(AugurSpaceship spaceship)
    {
        return ByAugurSpaceship.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(QuantumSpaceship spaceship)
    {
        return ByQuantumSpaceship.Spaceship(spaceship);
    }
}