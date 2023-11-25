using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class MeridianSpaceship : ISpaceship
{
    private const double MeridianSpaceshipOverallCharacteristics = 200;
    private const double MeridianSpaceshipDeflectorPoints = 600;

    public MeridianSpaceship(float impulseEngineInitialFuelLevel)
    {
        ImpulseEngine = new EImpulseEngine(impulseEngineInitialFuelLevel);
    }

    public ImpulseEngine ImpulseEngine { get; }
    public JumpEngine JumpEngine { get; } = new NoneJumpEngine();
    public IEmitter Emitter { get; } = new AntiNeutrinoEmitter();
    public IDeflector Deflector { get; } = new SecondClassDeflector(MeridianSpaceshipDeflectorPoints);
    public IArmor Armor { get; } = new SecondArmorClass(MeridianSpaceshipOverallCharacteristics);

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