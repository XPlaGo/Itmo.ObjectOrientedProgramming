using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Validation;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Result;

public class OptimalSpaceshipResult
{
    public OptimalSpaceshipResult(SpaceshipResultResponse response, int index = -1)
    {
        DoubleValidationException.ThrowIfLessThan(index, -1);
        Index = index;
        Response = response;
    }

    public int Index { get; }
    public SpaceshipResultResponse? Response { get; }
}