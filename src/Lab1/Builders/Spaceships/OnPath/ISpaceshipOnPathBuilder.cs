using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Spaceships.OnPath;

public interface ISpaceshipOnPathBuilder<TResult, out TSpaceship>
{
    ISpaceshipOnPathBuilder<TResult, TSpaceship> OnPath(Func<TSpaceship, SequentialPath, TResult> fly);
    SpaceshipOnPathDispatcher<TResult> Confirm();
}