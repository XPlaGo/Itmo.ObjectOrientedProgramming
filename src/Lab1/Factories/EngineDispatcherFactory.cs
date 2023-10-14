using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Factories;

public class EngineDispatcherFactory
{
    public IEngineVisitor<IEnvironmentVisitor<FlightResultResponse>> CreateDispatcher()
    {
        return new EngineDispatcher<FlightResultResponse>(FlyBlock)
            .WithCImpulseEngine
                .ThroughSpace(Fly)
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .ThroughNebulaeOfNitrideParticles(Fly)
                .Confirm()
            .WithEImpulseEngine
                .ThroughSpace(Fly)
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .ThroughNebulaeOfNitrideParticles(Fly)
                .Confirm()
            .WithAlphaJumpEngine
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .WithGammaJumpEngine
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .WithOmegaJumpEngine
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm();
    }

    private static FlightResultResponse FlyBlock(IEngine engine, IEnvironment environment)
    {
        return new FlightResultResponse(FlightResult.EngineNotSuit);
    }

    private static FlightResultResponse FlyTemplate(IEngine engine, IEnvironment environment, double effectiveness)
    {
        return engine.Fly(environment, effectiveness);
    }

    private FlightResultResponse Fly(ImpulseEngine engine, Space environment)
    {
        return FlyTemplate(engine, environment, 1);
    }

    private FlightResultResponse Fly(ImpulseEngine engine, NebulaeOfNitrideParticles environment)
    {
        return FlyTemplate(engine, environment, 0.5);
    }

    private FlightResultResponse Fly(ImpulseEngine engine, NebulaeOfIncreasedDensityOfSpace environment)
    {
        return FlyTemplate(engine, environment, 0.01);
    }

    private FlightResultResponse Fly(JumpEngine engine, NebulaeOfIncreasedDensityOfSpace environment)
    {
        return FlyTemplate(engine, environment, 100);
    }
}