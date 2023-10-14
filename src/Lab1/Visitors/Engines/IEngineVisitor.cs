using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

public interface IEngineVisitor<out T>
{
    T Fly(CImpulseEngine engine);

    T Fly(EImpulseEngine engine);

    T Fly(AlphaJumpEngine engine);

    T Fly(OmegaJumpEngine engine);

    T Fly(GammaJumpEngine engine);
}