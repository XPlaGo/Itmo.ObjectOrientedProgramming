using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

public interface IPath : ICloneable
{
    IList<IEnvironment> Environments { get; }
    T AcceptPathVisitor<T>(IPathVisitor<T> visitor);
}