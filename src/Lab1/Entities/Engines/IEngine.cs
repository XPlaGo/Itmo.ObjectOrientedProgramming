using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;

public interface IEngine
{
    public double CurrentVelocity { get; }

    public double FuelConsumption { get; }

    public double LaunchTime { get; }

    public double FuelLevel { get; protected set; }

    public double FillUpFuelLevel(double fillUpValue);

    public FlightResultResponse Fly(IEnvironment environment, double effectiveness);

    T AcceptEngineVisitor<T>(IEngineVisitor<T> visitor);

    public void UpdateVelocity(double value);
}