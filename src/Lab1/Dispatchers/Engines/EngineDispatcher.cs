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
    public EngineDispatcher(Func<IEngine, IEnvironment, TFlightResult> fly)
    {
        WithCImpulseEngine = new EngineFlightBuilder<TFlightResult, CImpulseEngine>(this, fly);
        WithEImpulseEngine = new EngineFlightBuilder<TFlightResult, EImpulseEngine>(this, fly);
        WithAlphaJumpEngine = new EngineFlightBuilder<TFlightResult, AlphaJumpEngine>(this, fly);
        WithGammaJumpEngine = new EngineFlightBuilder<TFlightResult, GammaJumpEngine>(this, fly);
        WithOmegaJumpEngine = new EngineFlightBuilder<TFlightResult, OmegaJumpEngine>(this, fly);
        WithNoneImpulseEngine = new EngineFlightBuilder<TFlightResult, NoneImpulseEngine>(this, fly);
        WithNoneJumpEngine = new EngineFlightBuilder<TFlightResult, NoneJumpEngine>(this, fly);
    }

    public EngineFlightBuilder<TFlightResult, CImpulseEngine> WithCImpulseEngine { get; }
    public EngineFlightBuilder<TFlightResult, EImpulseEngine> WithEImpulseEngine { get; }
    public EngineFlightBuilder<TFlightResult, AlphaJumpEngine> WithAlphaJumpEngine { get; }
    public EngineFlightBuilder<TFlightResult, GammaJumpEngine> WithGammaJumpEngine { get; }
    public EngineFlightBuilder<TFlightResult, OmegaJumpEngine> WithOmegaJumpEngine { get; }
    public EngineFlightBuilder<TFlightResult, NoneImpulseEngine> WithNoneImpulseEngine { get; }
    public EngineFlightBuilder<TFlightResult, NoneJumpEngine> WithNoneJumpEngine { get; }

    public IEnvironmentVisitor<TFlightResult> Fly(CImpulseEngine engine)
    {
        return WithCImpulseEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(EImpulseEngine engine)
    {
        return WithEImpulseEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(AlphaJumpEngine engine)
    {
        return WithAlphaJumpEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(OmegaJumpEngine engine)
    {
        return WithOmegaJumpEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(GammaJumpEngine engine)
    {
        return WithGammaJumpEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(NoneImpulseEngine engine)
    {
        return WithNoneImpulseEngine.Engine(engine);
    }

    public IEnvironmentVisitor<TFlightResult> Fly(NoneJumpEngine engine)
    {
        return WithNoneJumpEngine.Engine(engine);
    }
}