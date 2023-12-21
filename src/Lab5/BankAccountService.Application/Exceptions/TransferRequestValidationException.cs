namespace BankAccountService.Application.Exceptions;

public class TransferRequestValidationException : Exception
{
    public TransferRequestValidationException(string message)
        : base(message)
    {
    }

    public TransferRequestValidationException()
    {
    }

    public TransferRequestValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}