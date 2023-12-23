namespace BankAccountService.Application.Exceptions;

public class CurrencyNotFoundException : Exception
{
    public CurrencyNotFoundException(string message)
        : base(message)
    {
    }

    public CurrencyNotFoundException()
    {
    }

    public CurrencyNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CurrencyNotFoundException(long code)
        : base($"Currency with code {code} not found")
    {
    }
}