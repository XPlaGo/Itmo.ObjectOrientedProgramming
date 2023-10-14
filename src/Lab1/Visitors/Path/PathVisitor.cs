using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

public class PathVisitor : IPathVisitor<SpaceshipResult>
{
    public SpaceshipResult Visit(Entities.Path.Path path)
    {
        throw new System.NotImplementedException();
    }
}