using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Builders;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.ThroughEnvironment;

public class SpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> : ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship>,
    IEnvironmentVisitor<TResult>
    where TSpaceship : ISpaceship
{
    private readonly Func<TSpaceship, IEnvironment, TResult> _fly;
    private Func<TSpaceship, Space, TResult> _spaceFly;
    private Func<TSpaceship, NebulaeOfIncreasedDensityOfSpace, TResult> _nebulaeOfIncreasedDensityOfSpaceFly;
    private Func<TSpaceship, NebulaeOfNitrideParticles, TResult> _nebulaeOfNitrideParticlesFly;

    private TSpaceship? _spaceship;

    public SpaceshipThroughEnvironmentBuilder(
        SpaceshipThroughEnvironmentDispatcher<TResult> dispatcher,
        Func<TSpaceship, IEnvironment, TResult> fly)
    {
        Dispatcher = dispatcher;
        _fly = fly;

        _spaceFly = (spaceship, environment) => fly(spaceship, environment);
        _nebulaeOfIncreasedDensityOfSpaceFly = (spaceship, environment) => fly(spaceship, environment);
        _nebulaeOfNitrideParticlesFly = (spaceship, environment) => fly(spaceship, environment);
    }

    private SpaceshipThroughEnvironmentDispatcher<TResult> Dispatcher { get; }

    public IEnvironmentVisitor<TResult> Spaceship(TSpaceship spaceship)
    {
        _spaceship = spaceship;
        return this;
    }

    public ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughSpace(Func<TSpaceship, Space, TResult> fly)
    {
        _spaceFly = fly;
        return this;
    }

    public ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughNebulaeOfIncreasedDensityOfSpace(Func<TSpaceship, NebulaeOfIncreasedDensityOfSpace, TResult> fly)
    {
        _nebulaeOfIncreasedDensityOfSpaceFly = fly;
        return this;
    }

    public ISpaceshipThroughEnvironmentBuilder<TResult, TSpaceship> ThroughNebulaeOfNitrideParticles(Func<TSpaceship, NebulaeOfNitrideParticles, TResult> fly)
    {
        _nebulaeOfNitrideParticlesFly = fly;
        return this;
    }

    public SpaceshipThroughEnvironmentDispatcher<TResult> Confirm()
    {
        return Dispatcher;
    }

    public TResult Visit(Space environment)
    {
        if (_spaceship is null) throw new BuildComponentCannotBeNull(nameof(_spaceship));
        return _spaceFly(_spaceship, environment);
    }

    public TResult Visit(NebulaeOfIncreasedDensityOfSpace environment)
    {
        if (_spaceship is null) throw new BuildComponentCannotBeNull(nameof(_spaceship));
        return _nebulaeOfIncreasedDensityOfSpaceFly(_spaceship, environment);
    }

    public TResult Visit(NebulaeOfNitrideParticles environment)
    {
        if (_spaceship is null) throw new BuildComponentCannotBeNull(nameof(_spaceship));
        return _nebulaeOfNitrideParticlesFly(_spaceship, environment);
    }
}