using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;

public interface ISpaceshipOnPathVisitor<out T>
{
    T Visit(ISpaceship spaceship);
}