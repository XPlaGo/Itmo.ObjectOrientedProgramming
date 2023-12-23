namespace BankAccountService.Application.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string accountType, long accountId)
        : base($"{accountType} with id {accountId} not found")
    { }

    public AccountNotFoundException(string message)
        : base(message)
    {
    }

    public AccountNotFoundException()
    {
    }

    public AccountNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}