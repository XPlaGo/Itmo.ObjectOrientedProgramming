using System;
using System.Data;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Builders;

public class BuildComponentCannotBeNull : NoNullAllowedException
{
    public BuildComponentCannotBeNull(string message)
        : base(message) { }

    public BuildComponentCannotBeNull() { }

    public BuildComponentCannotBeNull(string message, Exception innerException)
        : base(message, innerException) { }
}