namespace BankAccountService.Application.Interfaces.Services;

public interface IAuthTokenService
{
    Task<string> GenerateInternalAccessToken(string serviceName);
}