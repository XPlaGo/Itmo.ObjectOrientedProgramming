using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

public class FlightResultData
{
    public FlightResultData(double time, double currentVelocity, double length, double fuel)
    {
        DoubleValidationException.ThrowIfLessThan(time, 0);
        DoubleValidationException.ThrowIfLessThan(currentVelocity, 0);
        DoubleValidationException.ThrowIfLessThan(length, 0);
        DoubleValidationException.ThrowIfLessThan(fuel, 0);

        Time = time;
        CurrentVelocity = currentVelocity;
        Length = length;
        Fuel = fuel;
    }

    public double Time { get; set; }
    public double CurrentVelocity { get; set; }
    public double Length { get; set; }
    public double Fuel { get; set; }

    public static FlightResultData Empty()
    {
        return new FlightResultData(0, 0, 0, 0);
    }
}