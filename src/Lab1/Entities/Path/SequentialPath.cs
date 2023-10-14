using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

public class SequentialPath : IPath
{
    public SequentialPath(IReadOnlyCollection<IEnvironment> environments)
    {
        Environments = environments;
    }

    public SequentialPath(params IEnvironment[] environments)
    {
        Environments = environments.ToList();
    }

    public IReadOnlyCollection<IEnvironment> Environments { get; }
    public T AcceptPathVisitor<T>(IPathVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }

    public object Clone()
    {
        return new SequentialPath(Environments.Select(e => (IEnvironment)e.Clone()).ToList());
    }
}