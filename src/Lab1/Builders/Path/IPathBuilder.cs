using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.Path;

public interface IPathBuilder<out TPath>
    where TPath : IPath
{
    TPath Build();
    IPathBuilder<TPath> AddEnvironment(IEnvironment environment);
}