using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;

public class DoubleValidationException : ArgumentException
{
    public DoubleValidationException(string message)
        : base(message) { }

    public DoubleValidationException() { }

    public DoubleValidationException(string message, Exception innerException)
        : base(message, innerException) { }

    public static void ThrowIfLessThan(double value, double min)
    {
        if (value < min) throw new DoubleValidationException($"{value} is less than {min}");
    }

    public static void ThrowIfLessOrEqThan(double value, double min)
    {
        if (value <= min) throw new DoubleValidationException($"{value} is less of equal than {min}");
    }

    public static void ThrowIfGreaterThan(double value, double max)
    {
        if (value > max) throw new DoubleValidationException($"{value} is greater than {max}");
    }

    public static void ThrowIfGreaterOrEqThan(double value, double max)
    {
        if (value >= max) throw new DoubleValidationException($"{value} is greater or equal than {max}");
    }
}