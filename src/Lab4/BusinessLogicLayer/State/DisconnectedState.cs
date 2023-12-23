using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Element.Directory;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Exceptions.State;
using Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.Service.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.BusinessLogicLayer.State;

public class DisconnectedState<TCommandResponse> : IConnectionState
{
    private readonly IFileSystemService<TCommandResponse> _service;

    public DisconnectedState(IFileSystemService<TCommandResponse> service)
    {
        _service = service;
    }

    public IDirectory Connect()
    {
        return _service.Connect();
    }

    public void Disconnect()
    {
        throw new AlreadyDisconnectedException();
    }
}