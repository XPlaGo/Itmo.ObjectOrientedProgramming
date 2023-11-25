using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.UserEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;

public class UserAddressee : IAddressee
{
    private readonly IUser _user;

    public UserAddressee(IUser user)
    {
        _user = user;
    }

    public int MinImportanceLevel => 3;

    public void HandleMessage(IMessage<IPrintable> message)
    {
        _user.HandleMessage(message);
    }

    public string GetPrefix()
    {
        return _user.Prefix;
    }
}