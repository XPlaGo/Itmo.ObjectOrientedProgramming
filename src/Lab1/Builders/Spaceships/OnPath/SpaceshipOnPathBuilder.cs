using System;
using System.Data;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;

public class SpaceshipOnPathBuilder<TResult, TSpaceship> : ISpaceshipOnPathBuilder<TResult, TSpaceship>,
    IPathVisitor<TResult>
    where TSpaceship : ISpaceship
{
    private readonly Func<TSpaceship, IPath, TResult> _fly;
    private Func<TSpaceship, Entities.Path.Path, TResult> _pathFly;

    private TSpaceship? _spaceship;

    public SpaceshipOnPathBuilder(
        SpaceshipOnPathDispatcher<TResult> dispatcher,
        Func<TSpaceship, IPath, TResult> fly)
    {
        Dispatcher = dispatcher;
        _fly = fly;

        _pathFly = (a, b) => fly(a, b);
    }

    public SpaceshipOnPathDispatcher<TResult> Dispatcher { get; }

    public IPathVisitor<TResult> Spaceship(TSpaceship spaceship)
    {
        _spaceship = spaceship;
        return this;
    }

    public ISpaceshipOnPathBuilder<TResult, TSpaceship> OnPath(Func<TSpaceship, Entities.Path.Path, TResult> fly)
    {
        _pathFly = fly;
        return this;
    }

    public SpaceshipOnPathDispatcher<TResult> Confirm()
    {
        return Dispatcher;
    }

    public TResult Visit(Entities.Path.Path path)
    {
        if (_spaceship == null) throw new NoNullAllowedException(nameof(_spaceship));
        return _pathFly(_spaceship, path);
    }
}