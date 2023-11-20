using System;

namespace Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Validator;

public class PcAssemblyValidatorException : Exception
{
    public PcAssemblyValidatorException(string message)
        : base(message)
    { }

    public PcAssemblyValidatorException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public PcAssemblyValidatorException()
    {
    }
}