using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;

public class SpaceshipOnPathDispatcher<TResult> : ISpaceshipOnPathVisitor<IPathVisitor<TResult>>
{
    public SpaceshipOnPathDispatcher(Func<ISpaceship, IPath, TResult> fly)
    {
        BySpaceship = new SpaceshipOnPathBuilder<TResult, ISpaceship>(this, fly);
    }

    public SpaceshipOnPathBuilder<TResult, ISpaceship> BySpaceship { get; }

    public IPathVisitor<TResult> Visit(ISpaceship spaceship)
    {
        return BySpaceship.Spaceship(spaceship);
    }
}