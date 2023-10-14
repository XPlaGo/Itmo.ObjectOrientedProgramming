namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;

public interface IPathVisitor<out T>
{
    T Visit(Entities.Path.Path path);
}