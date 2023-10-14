using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;

public interface ISpaceshipOnPathBuilder<TResult, out TSpaceship>
{
    ISpaceshipOnPathBuilder<TResult, TSpaceship> OnPath(Func<TSpaceship, Entities.Path.Path, TResult> fly);
    SpaceshipOnPathDispatcher<TResult> Confirm();
}