using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Path;

public class PathBuilder : IPathBuilder<SequentialPath>
{
    private readonly List<IEnvironment> _environments = new();

    public SequentialPath Build()
    {
        return new SequentialPath(_environments);
    }

    public IPathBuilder<SequentialPath> AddEnvironment(IEnvironment environment)
    {
        _environments.Add(environment);
        return this;
    }
}