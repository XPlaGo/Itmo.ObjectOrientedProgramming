using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.State;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

public class ConnectedState<TCommandResponse> : IConnectionState
{
    private readonly IFileSystemService<TCommandResponse> _service;

    public ConnectedState(IFileSystemService<TCommandResponse> service)
    {
        _service = service;
    }

    public IDirectory Connect()
    {
        throw new AlreadyConnectedException();
    }

    public void Disconnect()
    {
        _service.Disconnect();
    }
}