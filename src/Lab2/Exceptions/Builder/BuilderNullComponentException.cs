using System.Data;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;

public class BuilderNullComponentException : NoNullAllowedException
{
    public BuilderNullComponentException(string message)
        : base(message)
    { }

    public BuilderNullComponentException()
    { }

    public BuilderNullComponentException(string message, System.Exception innerException)
        : base(message, innerException)
    { }
}