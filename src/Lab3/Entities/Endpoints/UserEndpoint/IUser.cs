using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Message.Content;
using Itmo.ObjectOrientedProgramming.Lab3.Models.User;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.UserEndpoint;

public interface IUser
{
    public string Username { get; }

    public string Prefix { get; }

    public void HandleMessage(IMessage<IPrintable> message);

    public void ReadMessageByIndex(int index);

    public MessageStatus GetMessageStatusByIndex(int index);
}