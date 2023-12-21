namespace BankAccountService.Application.Exceptions;

public class ServiceException : Exception
{
    public ServiceException(string message)
        : base(message) { }

    public ServiceException()
    {
    }

    public ServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ServiceException(IReadOnlyList<string> messages)
    {
        Messages = messages;
    }

    public IReadOnlyList<string> Messages { get; set; } = new List<string>();
}