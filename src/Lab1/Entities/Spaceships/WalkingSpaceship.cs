using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class WalkingSpaceship : ISpaceship
{
    private const double WalkingSpaceshipOverallCharacteristics = 100;

    public WalkingSpaceship(float impulseEngineInitialFuelLevel, double initialVelocity)
    {
        ImpulseEngine = new CImpulseEngine(impulseEngineInitialFuelLevel);
        ISpaceship spaceship = this;
        spaceship.UpdateVelocity(initialVelocity);
    }

    public ImpulseEngine ImpulseEngine { get; }
    public JumpEngine JumpEngine { get; } = new NoneJumpEngine();
    public IEmitter Emitter { get; } = new NoneEmitter();
    public IDeflector Deflector { get; } = new NoneDeflector();
    public IArmor Armor { get; } = new FirstArmorClass(WalkingSpaceshipOverallCharacteristics);

    public T AcceptSpaceshipVisitor<T>(ISpaceshipThroughEnvironmentVisitor<T> throughEnvironmentVisitor)
    {
        ArgumentNullException.ThrowIfNull(throughEnvironmentVisitor);
        return throughEnvironmentVisitor.Visit(this);
    }

    public T AcceptSpaceshipVisitor<T>(ISpaceshipOnPathVisitor<T> throughEnvironmentVisitor)
    {
        ArgumentNullException.ThrowIfNull(throughEnvironmentVisitor);
        return throughEnvironmentVisitor.Visit(this);
    }
}