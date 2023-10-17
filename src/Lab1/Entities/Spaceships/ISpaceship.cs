using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public interface ISpaceship
{
    ImpulseEngine? ImpulseEngine { get; }
    JumpEngine? JumpEngine { get; }
    IEmitter? Emitter { get; }
    IDeflector? Deflector { get; }
    IArmor? Armor { get; }

    public void UpdateVelocity(double value)
    {
        ImpulseEngine?.UpdateVelocity(value);
        JumpEngine?.UpdateVelocity(value);
    }

    T AcceptSpaceshipVisitor<T>(ISpaceshipThroughEnvironmentVisitor<T> throughEnvironmentVisitor);
    T AcceptSpaceshipVisitor<T>(ISpaceshipOnPathVisitor<T> throughEnvironmentVisitor);
}