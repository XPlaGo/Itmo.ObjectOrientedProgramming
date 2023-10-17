using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class QuantumSpaceship : ISpaceship
{
    private const double QuantumSpaceshipOverallCharacteristics = 200;
    private const double QuantumSpaceshipDeflectorPoints = 500;

    public QuantumSpaceship(float impulseEngineInitialFuelLevel, float jumpEngineInitialFuelLevel)
    {
        ImpulseEngine = new EImpulseEngine(impulseEngineInitialFuelLevel);
        JumpEngine = new OmegaJumpEngine(jumpEngineInitialFuelLevel);
    }

    public ImpulseEngine ImpulseEngine { get; }
    public JumpEngine JumpEngine { get; }
    public IEmitter Emitter { get; } = new AntiNeutrinoEmitter();

    public IDeflector Deflector { get; } =
        new PhotonDeflector(QuantumSpaceshipDeflectorPoints, QuantumSpaceshipDeflectorPoints);

    public IArmor Armor { get; } = new ThirdArmorClass(QuantumSpaceshipOverallCharacteristics);

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