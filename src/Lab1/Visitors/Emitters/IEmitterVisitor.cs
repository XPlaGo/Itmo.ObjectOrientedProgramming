using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

public interface IEmitterVisitor<out T>
{
    T Visit(AntiNeutrinoEmitter emitter);
}