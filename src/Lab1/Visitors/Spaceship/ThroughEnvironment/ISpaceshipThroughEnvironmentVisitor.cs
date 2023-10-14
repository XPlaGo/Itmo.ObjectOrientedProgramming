using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

public interface ISpaceshipThroughEnvironmentVisitor<out T>
{
    T Visit(WalkingSpaceship spaceship);
    T Visit(VaclasSpaceship spaceship);
    T Visit(MeridianSpaceship spaceship);
    T Visit(StellaSpaceship spaceship);
    T Visit(AugurSpaceship spaceship);
    T Visit(QuantumSpaceship spaceship);
}