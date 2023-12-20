namespace IdentityService.Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
        : base("User Already Exists")
    {
    }

    public UserAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public UserAlreadyExistsException(string message)
        : base(message)
    {
    }
}