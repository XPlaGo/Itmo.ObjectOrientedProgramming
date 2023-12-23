namespace BankAccountService.Application.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(string message)
        : base(message)
    {
    }

    public NotEnoughMoneyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotEnoughMoneyException()
        : base("Not enough money")
    {
    }
}