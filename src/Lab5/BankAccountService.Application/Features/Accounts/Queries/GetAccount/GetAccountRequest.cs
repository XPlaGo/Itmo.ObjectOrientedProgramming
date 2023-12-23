namespace BankAccountService.Application.Features.Accounts.Queries.GetAccount;

public class GetAccountRequest
{
    public long AccountId { get; set; }
    public long UserId { get; set; }
}