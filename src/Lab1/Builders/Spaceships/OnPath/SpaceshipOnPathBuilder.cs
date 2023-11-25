using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Builders;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;

public class SpaceshipOnPathBuilder<TResult, TSpaceship> : ISpaceshipOnPathBuilder<TResult, TSpaceship>,
    IPathVisitor<TResult>
    where TSpaceship : ISpaceship
{
    private readonly Func<TSpaceship, IPath, TResult> _fly;
    private Func<TSpaceship, SequentialPath, TResult> _pathFly;

    private TSpaceship? _spaceship;

    public SpaceshipOnPathBuilder(
        SpaceshipOnPathDispatcher<TResult> dispatcher,
        Func<TSpaceship, IPath, TResult> fly)
    {
        Dispatcher = dispatcher;
        _fly = fly;

        _pathFly = (spaceship, path) => fly(spaceship, path);
    }

    private SpaceshipOnPathDispatcher<TResult> Dispatcher { get; }

    public IPathVisitor<TResult> Spaceship(TSpaceship spaceship)
    {
        _spaceship = spaceship;
        return this;
    }

    public ISpaceshipOnPathBuilder<TResult, TSpaceship> OnPath(Func<TSpaceship, SequentialPath, TResult> fly)
    {
        _pathFly = fly;
        return this;
    }

    public SpaceshipOnPathDispatcher<TResult> Confirm()
    {
        return Dispatcher;
    }

    public TResult Visit(SequentialPath sequentialPath)
    {
        if (_spaceship is null) throw new BuildComponentCannotBeNull(nameof(_spaceship));
        return _pathFly(_spaceship, sequentialPath);
    }
}