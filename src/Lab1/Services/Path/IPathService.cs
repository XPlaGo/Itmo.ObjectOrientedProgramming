using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services.Path;

public interface IPathService
{
    SpaceshipResultResponse Fly(ISpaceship spaceship, IPath path);

    OptimalSpaceshipResult GetOptimal(IReadOnlyCollection<ISpaceship> spaceships, IPath path);
}