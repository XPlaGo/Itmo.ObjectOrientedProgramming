using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armor;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class VaclasSpaceship : Spaceship
{
    public VaclasSpaceship(
        float impulseEngineInitialFuelLevel,
        float jumpEngineInitialFuelLevel,
        bool hasDeflector = true,
        bool usePhotonDeflector = false)
    {
        ImpulseEngine = new EImpulseEngine(impulseEngineInitialFuelLevel);
        JumpEngine = new GammaJumpEngine(jumpEngineInitialFuelLevel);
        if (hasDeflector)
            Deflector = usePhotonDeflector ? new PhotonDeflector(300, 300) : new FirstClassDeflector(300);
    }

    public override ImpulseEngine ImpulseEngine { get; }
    public override JumpEngine JumpEngine { get; }
    public override IEmitter? Emitter { get; }
    public override IDeflector? Deflector { get; }
    public override IArmor Armor { get; } = new SecondArmorClass(200);

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