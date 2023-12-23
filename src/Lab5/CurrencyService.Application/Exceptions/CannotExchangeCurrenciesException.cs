namespace CurrencyService.Application.Exceptions;

public class CannotExchangeCurrenciesException : Exception
{
    public CannotExchangeCurrenciesException(string message)
        : base(message)
    {
    }

    public CannotExchangeCurrenciesException()
    {
    }

    public CannotExchangeCurrenciesException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CannotExchangeCurrenciesException(long fromCurrencyCode, long toCurrency, string reason)
        : base($"Cannot exchange from currency with code {fromCurrencyCode} to currency with code {toCurrency}. Reason: {reason}")
    {
    }
}