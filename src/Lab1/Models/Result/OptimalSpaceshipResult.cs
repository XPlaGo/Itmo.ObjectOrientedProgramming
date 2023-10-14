using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result;

public class OptimalSpaceshipResult
{
    public OptimalSpaceshipResult(int index = -1, SpaceshipResultResponse? response = null)
    {
        Index = index;
        Response = response;
    }

    public int Index { get; init; }
    public SpaceshipResultResponse? Response { get; init; }
}