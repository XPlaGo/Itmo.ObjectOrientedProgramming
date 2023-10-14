using System;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Result;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

public class SpaceshipResultResponse
{
    public SpaceshipResultResponse(SpaceshipResult spaceshipResult, SpaceshipResultData spaceshipResultData)
    {
        ArgumentNullException.ThrowIfNull(spaceshipResultData);
        SpaceshipResult = spaceshipResult;
        SpaceshipResultData = spaceshipResultData;
    }

    public SpaceshipResultResponse(FlightResultResponse flightResultResponse)
    {
        ArgumentNullException.ThrowIfNull(flightResultResponse);

        SpaceshipResult = flightResultResponse.FlightResult switch
        {
            FlightResult.FlightResult.Overcome => SpaceshipResult.Overcome,
            FlightResult.FlightResult.PartiallyOvercome => SpaceshipResult.NotEnoughFuel,
            FlightResult.FlightResult.FuelRanOut => SpaceshipResult.NotEnoughFuel,
            FlightResult.FlightResult.EngineNotSuit => SpaceshipResult.NoSuitableEngines,
            FlightResult.FlightResult.None => SpaceshipResult.None,
            _ => throw new ConvertException(flightResultResponse.FlightResult.GetType().Name),
        };

        SpaceshipResultData = new SpaceshipResultData(
            flightResultResponse.FlightResultData.Time,
            flightResultResponse.FlightResultData.CurrentVelocity,
            flightResultResponse.FlightResultData.Length,
            flightResultResponse.FlightResultData.Fuel);
    }

    public SpaceshipResult SpaceshipResult { get; set; }
    public SpaceshipResultData SpaceshipResultData { get; set; }
}