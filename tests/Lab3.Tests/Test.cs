using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.DisplayEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.MessengerEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Endpoints.UserEndpoint;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Message;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Topic;
using Itmo.ObjectOrientedProgramming.Lab3.Exceptions.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Factories.Addressee;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Display.DisplayDriver;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messenger.MessengerService;
using Itmo.ObjectOrientedProgramming.Lab3.Models.User;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class Test
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AddresseeLoggerFilteredFactory _addresseeFactory;

    public Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _addresseeFactory = new AddresseeLoggerFilteredFactory(
            new TestOutputLogger(_testOutputHelper));
    }

    [Fact]
    public void SendMessageToUser()
    {
        var user = new User("xplago");

        FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>> userAddressee = _addresseeFactory.CreateUserAddressee(user);

        var topic = new DefaultTopic("news", userAddressee);
        topic.SendMessage(new TextMessage("hello there", 3));

        Assert.Equal(MessageStatus.Unread, user.GetMessageStatusByIndex(0));
    }

    [Fact]
    public void SendMessageToUserAndRead()
    {
        var user = new User("xplago");

        FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>> userAddressee = _addresseeFactory.CreateUserAddressee(user);

        var topic = new DefaultTopic("news", userAddressee);
        topic.SendMessage(new TextMessage("hello there", 3));

        user.ReadMessageByIndex(0);

        Assert.Equal(MessageStatus.Read, user.GetMessageStatusByIndex(0));
    }

    [Fact]
    public void SendMessageToUserAndReadException()
    {
        var user = new User("xplago");

        FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>> userAddressee = _addresseeFactory.CreateUserAddressee(user);

        var topic = new DefaultTopic("news", userAddressee);
        topic.SendMessage(new TextMessage("hello there", 3));

        user.ReadMessageByIndex(0);

        Assert.Throws<MessageAlreadyReadException>(Action);
        return;

        void Action() => user.ReadMessageByIndex(0);
    }

    [Fact]
    public void MessageFiltered()
    {
        IUser userSubstitute = Substitute.For<IUser>();

        FilteredAddressee<AddresseeLoggerDecorator<UserAddressee>> userAddressee =
            _addresseeFactory.CreateUserAddressee(userSubstitute);

        var topic = new DefaultTopic("news", userAddressee);
        var message = new TextMessage("hello there", 2);
        topic.SendMessage(message);

        userSubstitute.DidNotReceive().HandleMessage(message);
    }

    [Fact]
    public void Logging()
    {
        IDisplay display = Substitute.For<IDisplay>();
        ILogger logger = Substitute.For<ILogger>();

        var addresseeFactory = new AddresseeLoggerFilteredFactory(logger);

        FilteredAddressee<AddresseeLoggerDecorator<DisplayAddressee>> displayAddressee =
            addresseeFactory.CreateDisplayAddressee(display);

        var topic = new DefaultTopic("news", displayAddressee);
        var message = new TextMessage("hello there", 3);

        topic.SendMessage(message);

        display.Received(1).ShowMessage(message);
        logger.Received(1).LogOut(Arg.Any<string>());
    }

    [Fact]
    public void MessengerServiceTest()
    {
        IMessengerService messengerService = Substitute.For<IMessengerService>();

        var messenger = new Messenger("xmess", "ru.xplago.xmess", messengerService);

        FilteredAddressee<AddresseeLoggerDecorator<MessengerAddressee>> messengerAddressee =
            _addresseeFactory.CreateMessengerAddressee(messenger);

        var topic = new DefaultTopic("news", messengerAddressee);
        var message = new TextMessage("hello there", 3);

        topic.SendMessage(message);

        messengerService.Received().Send(messenger.Assign, message.Body.Content.ToString());
    }

    [Fact]
    public void ExampleMessenger()
    {
        var messenger = new Messenger("xmess", "ru.xplago.xmess", new TestOutputMessengerService(_testOutputHelper));

        FilteredAddressee<AddresseeLoggerDecorator<MessengerAddressee>> messengerAddressee =
            _addresseeFactory.CreateMessengerAddressee(messenger);

        var topic = new DefaultTopic("news", messengerAddressee);
        var message = new TextMessage("hello there", 3);

        topic.SendMessage(message);
    }

    [Fact]
    public void ExampleDisplay()
    {
        var display = new Display("display", new TestOutputDisplayDriver(_testOutputHelper))
        {
            Color = Color.Green,
        };

        FilteredAddressee<AddresseeLoggerDecorator<DisplayAddressee>> displayAddressee =
            _addresseeFactory.CreateDisplayAddressee(display);

        var topic = new DefaultTopic("news", displayAddressee);
        var message = new TextMessage("hello there", 3);

        topic.SendMessage(message);
    }

    [Fact]
    public void ExampleGroup()
    {
        var display = new Display("display", new TestOutputDisplayDriver(_testOutputHelper))
        {
            Color = Color.Green,
        };
        var messenger = new Messenger("xmess", "ru.xplago.xmess", new TestOutputMessengerService(_testOutputHelper));

        FilteredAddressee<AddresseeLoggerDecorator<DisplayAddressee>> displayAddressee =
            _addresseeFactory.CreateDisplayAddressee(display);
        FilteredAddressee<AddresseeLoggerDecorator<MessengerAddressee>> messengerAddressee =
            _addresseeFactory.CreateMessengerAddressee(messenger);
        FilteredAddressee<AddresseeLoggerDecorator<GroupAddressee>> groupAddressee =
            _addresseeFactory.CreateGroupAddressee(new List<IAddressee> { messengerAddressee, displayAddressee });

        var topic = new DefaultTopic("news", groupAddressee);
        var message = new TextMessage("hello there", 3);

        topic.SendMessage(message);
    }
}