using System;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

public class FlightResultResponse
{
    public FlightResultResponse(FlightResult flightResult, FlightResultData flightResultData)
    {
        ArgumentNullException.ThrowIfNull(flightResultData);

        FlightResult = flightResult;
        FlightResultData = flightResultData;
    }

    public FlightResult FlightResult { get; set; }
    public FlightResultData FlightResultData { get; set; }
}