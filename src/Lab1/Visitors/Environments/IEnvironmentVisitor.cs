using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

public interface IEnvironmentVisitor<out T>
{
    T Visit(Space environment);
    T Visit(NebulaeOfIncreasedDensityOfSpace environment);
    T Visit(NebulaeOfNitrideParticles environment);
}