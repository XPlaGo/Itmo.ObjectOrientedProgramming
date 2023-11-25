using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;

public interface IEmitter
{
    T AcceptEmitterVisitor<T>(IEmitterVisitor<T> visitor);
}