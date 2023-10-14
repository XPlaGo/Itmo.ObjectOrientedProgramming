using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;

public class SpaceshipThroughEnvironmentDispatcher<TResult> : ISpaceshipThroughEnvironmentVisitor<IEnvironmentVisitor<TResult>>
{
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, WalkingSpaceship> _walkingSpaceshipBuilder;
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, VaclasSpaceship> _vaclasSpaceshipBuilder;
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, MeridianSpaceship> _meridianSpaceshipBuilder;
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, StellaSpaceship> _byStellaSpaceshipBuilder;
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, AugurSpaceship> _augurSpaceshipBuilder;
    private readonly SpaceshipThroughEnvironmentBuilder<TResult, QuantumSpaceship> _quantumSpaceshipBuilder;

    public SpaceshipThroughEnvironmentDispatcher(Func<ISpaceship, IEnvironment, TResult> fly)
    {
        _walkingSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, WalkingSpaceship>(this, fly);
        _vaclasSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, VaclasSpaceship>(this, fly);
        _meridianSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, MeridianSpaceship>(this, fly);
        _byStellaSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, StellaSpaceship>(this, fly);
        _augurSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, AugurSpaceship>(this, fly);
        _quantumSpaceshipBuilder = new SpaceshipThroughEnvironmentBuilder<TResult, QuantumSpaceship>(this, fly);
    }

    public ISpaceshipThroughEnvironmentBuilder<TResult, WalkingSpaceship> ByWalkingSpaceship => _walkingSpaceshipBuilder;
    public ISpaceshipThroughEnvironmentBuilder<TResult, VaclasSpaceship> ByVaclasSpaceship => _vaclasSpaceshipBuilder;
    public ISpaceshipThroughEnvironmentBuilder<TResult, MeridianSpaceship> ByMeridianSpaceship => _meridianSpaceshipBuilder;
    public ISpaceshipThroughEnvironmentBuilder<TResult, StellaSpaceship> ByStellaSpaceship => _byStellaSpaceshipBuilder;
    public ISpaceshipThroughEnvironmentBuilder<TResult, AugurSpaceship> ByAugurSpaceship => _augurSpaceshipBuilder;
    public ISpaceshipThroughEnvironmentBuilder<TResult, QuantumSpaceship> ByQuantumSpaceship => _quantumSpaceshipBuilder;

    public IEnvironmentVisitor<TResult> Visit(WalkingSpaceship spaceship)
    {
        return _walkingSpaceshipBuilder.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(VaclasSpaceship spaceship)
    {
        return _vaclasSpaceshipBuilder.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(MeridianSpaceship spaceship)
    {
        return _meridianSpaceshipBuilder.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(StellaSpaceship spaceship)
    {
        return _byStellaSpaceshipBuilder.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(AugurSpaceship spaceship)
    {
        return _augurSpaceshipBuilder.Spaceship(spaceship);
    }

    public IEnvironmentVisitor<TResult> Visit(QuantumSpaceship spaceship)
    {
        return _quantumSpaceshipBuilder.Spaceship(spaceship);
    }
}