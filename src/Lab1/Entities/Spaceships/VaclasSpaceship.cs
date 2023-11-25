using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Armors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;

public class VaclasSpaceship : ISpaceship
{
    private const double VaclasSpaceshipOverallCharacteristics = 200;
    private const double VaclasSpaceshipDeflectorPoints = 300;

    public VaclasSpaceship(
        float impulseEngineInitialFuelLevel,
        float jumpEngineInitialFuelLevel,
        bool hasDeflector = true,
        bool usePhotonDeflector = false)
    {
        ImpulseEngine = new EImpulseEngine(impulseEngineInitialFuelLevel);
        JumpEngine = new GammaJumpEngine(jumpEngineInitialFuelLevel);
        if (hasDeflector)
        {
            Deflector = usePhotonDeflector
                ? new PhotonDeflector(VaclasSpaceshipDeflectorPoints, VaclasSpaceshipDeflectorPoints)
                : new FirstClassDeflector(VaclasSpaceshipDeflectorPoints);
        }
    }

    public ImpulseEngine ImpulseEngine { get; }
    public JumpEngine JumpEngine { get; }
    public IEmitter Emitter { get; } = new NoneEmitter();
    public IDeflector Deflector { get; } = new NoneDeflector();
    public IArmor Armor { get; } = new SecondArmorClass(VaclasSpaceshipOverallCharacteristics);

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