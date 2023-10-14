using System;
using Itmo.ObjectOrientedProgramming.Lab1.Builders.EnginesFlight;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Engines;

public class EngineDispatcher<TFlightResult> : IEngineVisitor<IEnvironmentVisitor<TFlightResult>>
{
    private readonly EngineFlightBuilder<TFlightResult, CImpulseEngine> _cImpulseEngineBuilder;
    private readonly EngineFlightBuilder<TFlightResult, EImpulseEngine> _eImpulseEngineBuilder;
    private readonly EngineFlightBuilder<TFlightResult, AlphaJumpEngine> _alphaJumpEngineBuilder;
    private readonly EngineFlightBuilder<TFlightResult, GammaJumpEngine> _gammaJumpEngineBuilder;
    private readonly EngineFlightBuilder<TFlightResult, OmegaJumpEngine> _omegaJumpEngineBuilder;

    public EngineDispatcher(Func<IEngine, IEnvironment, TFlightResult> fly)
    {
        _cImpulseEngineBuilder = new EngineFlightBuilder<TFlightResult, CImpulseEngine>(this, fly);
        _eImpulseEngineBuilder = new EngineFlightBuilder<TFlightResult, EImpulseEngine>(this, fly);
        _alphaJumpEngineBuilder = new EngineFlightBuilder<TFlightResult, AlphaJumpEngine>(this, fly);
        _gammaJumpEngineBuilder = new EngineFlightBuilder<TFlightResult, GammaJumpEngine>(this, fly);
        _omegaJumpEngineBuilder = new EngineFlightBuilder<TFlightResult, OmegaJumpEngine>(this, fly);
    }

    public IEngineFlightBuilder<TFlightResult, CImpulseEngine> WithCImpulseEngine => _cImpulseEngineBuilder;
    public IEngineFlightBuilder<TFlightResult, EImpulseEngine> WithEImpulseEngine => _eImpulseEngineBuilder;
    public IEngineFlightBuilder<TFlightResult, AlphaJumpEngine> WithAlphaJumpEngine => _alphaJumpEngineBuilder;
    public IEngineFlightBuilder<TFlightResult, GammaJumpEngine> WithGammaJumpEngine => _gammaJumpEngineBuilder;
    public IEngineFlightBuilder<TFlightResult, OmegaJumpEngine> WithOmegaJumpEngine => _omegaJumpEngineBuilder;

    public IEnvironmentVisitor<TFlightResult> Fly(CImpulseEngine engine)
    {
        return _cImpulseEngineBuilder.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(EImpulseEngine engine)
    {
        return _eImpulseEngineBuilder.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(AlphaJumpEngine engine)
    {
        return _alphaJumpEngineBuilder.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(OmegaJumpEngine engine)
    {
        return _omegaJumpEngineBuilder.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(GammaJumpEngine engine)
    {
        return _gammaJumpEngineBuilder.Engine(engine);
    }
}