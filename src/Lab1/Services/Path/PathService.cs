using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Factories;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;

namespace Itmo.ObjectOrientedProgramming.Lab1.Services.Path;

public class PathService : IPathService
{
    private readonly ISpaceshipOnPathVisitor<IPathVisitor<SpaceshipResultResponse>> _dispatcher;

    public PathService()
    {
        var factory = new SpaceshipDispatcherFactory();
        _dispatcher = factory.CreateOnPathDispatcher();
    }

    public SpaceshipResultResponse Fly(ISpaceship spaceship, IPath path)
    {
        ArgumentNullException.ThrowIfNull(spaceship);
        ArgumentNullException.ThrowIfNull(path);
        return path.AcceptPathVisitor(spaceship.AcceptSpaceshipVisitor(_dispatcher));
    }

    public OptimalSpaceshipResult GetOptimal(IReadOnlyCollection<ISpaceship> spaceships, IPath path)
    {
        return spaceships
            .Select((spaceship, index) =>
            {
                var pathClone = (IPath)path.Clone();
                SpaceshipResultResponse response = Fly(spaceship, pathClone);
                return new OptimalSpaceshipResult(response, index);
            })
            .Where(response => response.Response is { SpaceshipResult: SpaceshipResult.Overcome, SpaceshipResultData: not null })
            .OrderBy(response =>
            {
                if (response.Response is not null)
                    return response.Response.SpaceshipResultData.Length;
                return 0;
            })
            .ThenByDescending(response =>
            {
                if (response.Response is { SpaceshipResultData: not null })
                    return response.Response.SpaceshipResultData.Fuel;
                return 0;
            })
            .ThenByDescending(response =>
            {
                if (response.Response is { SpaceshipResultData: not null })
                    return response.Response.SpaceshipResultData.Time;
                return 0;
            })
            .LastOrDefault(new OptimalSpaceshipResult(
                new SpaceshipResultResponse(
                    SpaceshipResult.None,
                    SpaceshipResultData.Empty())));
    }
}