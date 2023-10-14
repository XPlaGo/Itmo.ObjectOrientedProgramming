using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class QuantumSpaceship : Spaceship
{
    public QuantumSpaceship(float impulseEngineInitialFuelLevel, float jumpEngineInitialFuelLevel)
    {
        ImpulseEngine = new EImpulseEngine(impulseEngineInitialFuelLevel);
        JumpEngine = new OmegaJumpEngine(jumpEngineInitialFuelLevel);
    }

    public override ImpulseEngine ImpulseEngine { get; }
    public override JumpEngine JumpEngine { get; }
    public override IEmitter? Emitter { get; } = new AntiNeutrinoEmitter();
    public override IDeflector Deflector { get; } = new PhotonDeflector(500, 500);
    public override IArmor Armor { get; } = new ThirdArmorClass(200);

    public override T AcceptSpaceshipVisitor<T>(ISpaceshipThroughEnvironmentVisitor<T> throughEnvironmentVisitor)
    {
        ArgumentNullException.ThrowIfNull(throughEnvironmentVisitor);
        return throughEnvironmentVisitor.Visit(this);
    }

    public override T AcceptSpaceshipVisitor<T>(ISpaceshipOnPathVisitor<T> throughEnvironmentVisitor)
    {
        ArgumentNullException.ThrowIfNull(throughEnvironmentVisitor);
        return throughEnvironmentVisitor.Visit(this);
    }
}