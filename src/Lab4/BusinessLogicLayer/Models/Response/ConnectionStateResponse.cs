using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Models.Response;

public class ConnectionStateResponse
{
    public ConnectionStateResponse(IConnectionState state, IDirectory directory)
    {
        State = state;
        RootDirectory = directory;
    }

    public IConnectionState State { get; }
    public IDirectory RootDirectory { get; }
}