using System;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;

public class NoneEmitter : IEmitter
{
    public T AcceptEmitterVisitor<T>(IEmitterVisitor<T> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        return visitor.Visit(this);
    }
}