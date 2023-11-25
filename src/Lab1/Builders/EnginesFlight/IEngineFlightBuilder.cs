using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Builders.EnginesFlight;

public interface IEngineFlightBuilder<TFlightResult, out TEngine>
{
    IEngineFlightBuilder<TFlightResult, TEngine> ThroughSpace(Func<TEngine, Space, TFlightResult> fly);
    IEngineFlightBuilder<TFlightResult, TEngine> ThroughNebulaeOfIncreasedDensityOfSpace(Func<TEngine, NebulaeOfIncreasedDensityOfSpace, TFlightResult> fly);
    IEngineFlightBuilder<TFlightResult, TEngine> ThroughNebulaeOfNitrideParticles(Func<TEngine, NebulaeOfNitrideParticles, TFlightResult> fly);
    EngineDispatcher<TFlightResult> Confirm();
}