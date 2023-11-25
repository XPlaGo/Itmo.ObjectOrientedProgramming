using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.DisplayEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.MessengerEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.UserEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories.Addressee;

public class AddresseeLoggerFilteredFactory
{
    private readonly ILogger _logger;

    public AddresseeLoggerFilteredFactory(ILogger logger)
    {
        _logger = logger;
    }

    public FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>> CreateUserAddressee(IUser user)
    {
        return new FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>>(
            new AddresseeLoggerDecorator<UserAddressee>(
                new UserAddressee(user), _logger));
    }

    public FilteredAddressee<AddresseeLoggerDecorator<MessengerAddressee>> CreateMessengerAddressee(IMessenger messenger)
    {
        return new FilteredAddressee<AddresseeLoggerDecorator<MessengerAddressee>>(
            new AddresseeLoggerDecorator<MessengerAddressee>(
                new MessengerAddressee(messenger), _logger));
    }

    public FilteredAddressee<AddresseeLoggerDecorator<DisplayAddressee>> CreateDisplayAddressee(IDisplay display)
    {
        return new FilteredAddressee<AddresseeLoggerDecorator<DisplayAddressee>>(
            new AddresseeLoggerDecorator<DisplayAddressee>(
                new DisplayAddressee(display), _logger));
    }

    public FilteredAddressee<AddresseeLoggerDecorator<GroupAddressee>> CreateGroupAddressee(IReadOnlyList<IAddressee> addressees)
    {
        return new FilteredAddressee<AddresseeLoggerDecorator<GroupAddressee>>(
            new AddresseeLoggerDecorator<GroupAddressee>(
                new GroupAddressee(addressees), _logger));
    }
}