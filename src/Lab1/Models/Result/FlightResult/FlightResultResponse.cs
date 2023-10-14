namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;

public class FlightResultResponse
{
    public FlightResultResponse(FlightResult flightResult, FlightResultData? flightResultData = null)
    {
        FlightResult = flightResult;
        FlightResultData = flightResultData;
    }

    public FlightResult FlightResult { get; set; }
    public FlightResultData? FlightResultData { get; set; }
}