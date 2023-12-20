namespace BankAccountService.Application.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string accountType, long accountId, long userId)
        : base($"{accountType} with id {accountId} for user with id {userId} not found")
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