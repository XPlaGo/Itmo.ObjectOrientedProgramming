namespace BankAccountService.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommand
{
    public long UserId { get; set; }
    public long CurrencyCode { get; set; }
}