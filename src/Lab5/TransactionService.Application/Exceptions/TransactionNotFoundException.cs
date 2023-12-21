namespace TransactionService.Application.Exceptions;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException(string token)
        : base($"Transaction with TransactionToken = {token} not found")
    {
    }

    public TransactionNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public TransactionNotFoundException()
    {
    }
}