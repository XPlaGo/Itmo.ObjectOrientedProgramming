using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

public interface IConnectionState
{
    IDirectory Connect();
    void Disconnect();
}