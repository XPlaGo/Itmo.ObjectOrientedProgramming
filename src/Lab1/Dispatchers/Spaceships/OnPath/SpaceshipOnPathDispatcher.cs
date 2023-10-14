using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;

public class SpaceshipOnPathDispatcher<TResult> : ISpaceshipOnPathVisitor<IPathVisitor<TResult>>
{
    private readonly SpaceshipOnPathBuilder<TResult, ISpaceship> _spaceshipBuilder;

    public SpaceshipOnPathDispatcher(Func<ISpaceship, IPath, TResult> fly)
    {
        _spaceshipBuilder = new SpaceshipOnPathBuilder<TResult, ISpaceship>(this, fly);
    }

    public ISpaceshipOnPathBuilder<TResult, ISpaceship> BySpaceship => _spaceshipBuilder;
    public IPathVisitor<TResult> Visit(ISpaceship spaceship)
    {
        return _spaceshipBuilder.Spaceship(spaceship);
    }
}