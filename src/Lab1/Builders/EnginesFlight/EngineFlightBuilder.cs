using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Builders;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.EnginesFlight;

public class EngineFlightBuilder<TFlightResult, TEngine> : IEngineFlightBuilder<TFlightResult, TEngine>,
    IEnvironmentVisitor<TFlightResult>
    where TEngine : IEngine
{
    private readonly Func<TEngine, IEnvironment, TFlightResult> _fly;
    private Func<TEngine, Space, TFlightResult> _spaceFly;
    private Func<TEngine, NebulaeOfIncreasedDensityOfSpace, TFlightResult> _nebulaeOfIncreasedDensityOfSpaceFly;
    private Func<TEngine, NebulaeOfNitrideParticles, TFlightResult> _nebulaeOfNitrideParticlesFly;

    private TEngine? _engine;

    public EngineFlightBuilder(
        EngineDispatcher<TFlightResult> dispatcher,
        Func<TEngine, IEnvironment, TFlightResult> fly)
    {
        Dispatcher = dispatcher;
        _fly = fly;

        _spaceFly = (engine, environment) => fly(engine, environment);
        _nebulaeOfIncreasedDensityOfSpaceFly = (engine, environment) => fly(engine, environment);
        _nebulaeOfNitrideParticlesFly = (engine, environment) => fly(engine, environment);
    }

    private EngineDispatcher<TFlightResult> Dispatcher { get; }

    public IEnvironmentVisitor<TFlightResult> Engine(TEngine engine)
    {
        _engine = engine;
        return this;
    }

    public IEngineFlightBuilder<TFlightResult, TEngine> ThroughSpace(Func<TEngine, Space, TFlightResult> fly)
    {
        _spaceFly = fly;
        return this;
    }

    public IEngineFlightBuilder<TFlightResult, TEngine> ThroughNebulaeOfIncreasedDensityOfSpace(Func<TEngine, NebulaeOfIncreasedDensityOfSpace, TFlightResult> fly)
    {
        _nebulaeOfIncreasedDensityOfSpaceFly = fly;
        return this;
    }

    public IEngineFlightBuilder<TFlightResult, TEngine> ThroughNebulaeOfNitrideParticles(Func<TEngine, NebulaeOfNitrideParticles, TFlightResult> fly)
    {
        _nebulaeOfNitrideParticlesFly = fly;
        return this;
    }

    public EngineDispatcher<TFlightResult> Confirm()
    {
        return Dispatcher;
    }
#pragma warning restore SK1400

    public TFlightResult Visit(Space environment)
    {
        if (_engine is null) throw new BuildComponentCannotBeNull(nameof(_engine));
        return _spaceFly(_engine, environment);
    }

    public TFlightResult Visit(NebulaeOfIncreasedDensityOfSpace environment)
    {
        if (_engine is null) throw new BuildComponentCannotBeNull(nameof(_engine));
        return _nebulaeOfIncreasedDensityOfSpaceFly(_engine, environment);
    }

    public TFlightResult Visit(NebulaeOfNitrideParticles environment)
    {
        if (_engine is null) throw new BuildComponentCannotBeNull(nameof(_engine));
        return _nebulaeOfNitrideParticlesFly(_engine, environment);
    }
}