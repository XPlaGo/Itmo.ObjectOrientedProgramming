using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public abstract class Spaceship : ISpaceship
{
    public abstract ImpulseEngine ImpulseEngine { get; }
    public abstract JumpEngine JumpEngine { get; }
    public abstract IEmitter Emitter { get; }
    public abstract IDeflector Deflector { get; }
    public abstract IArmor Armor { get; }

    public double CurrentVelocity { get; private set; }

    public void UpdateVelocity(double value)
    {
        CurrentVelocity = value;
        ImpulseEngine?.UpdateVelocity(value);
        JumpEngine?.UpdateVelocity(value);
    }

    public abstract T AcceptSpaceshipVisitor<T>(ISpaceshipThroughEnvironmentVisitor<T> throughEnvironmentVisitor);
    public abstract T AcceptSpaceshipVisitor<T>(ISpaceshipOnPathVisitor<T> throughEnvironmentVisitor);
}