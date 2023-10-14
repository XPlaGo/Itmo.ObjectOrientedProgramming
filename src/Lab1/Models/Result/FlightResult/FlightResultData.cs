namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

public class FlightResultData
{
    public FlightResultData(double time, double currentVelocity, double length, double fuel)
    {
        Time = time;
        CurrentVelocity = currentVelocity;
        Length = length;
        Fuel = fuel;
    }

    public double Time { get; set; }
    public double CurrentVelocity { get; set; }
    public double Length { get; set; }
    public double Fuel { get; set; }
}