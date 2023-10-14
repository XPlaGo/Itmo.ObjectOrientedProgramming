using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Path;

public class PathBuilder : IPathBuilder<Entities.Path.Path>
{
    private readonly List<IEnvironment> _environments = new();

    public Entities.Path.Path Build()
    {
        return new Entities.Path.Path(_environments);
    }

    public IPathBuilder<Entities.Path.Path> AddEnvironment(IEnvironment environment)
    {
        _environments.Add(environment);
        return this;
    }
}