namespace BankAccountService.Application.Features.Accounts.Queries.GetAccount;

public record GetAccountRequest(long AccountId, long UserId);