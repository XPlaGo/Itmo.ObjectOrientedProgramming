using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

public interface IPathVisitor<out T>
{
    T Visit(SequentialPath sequentialPath);
}