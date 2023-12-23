namespace BankAccountService.Models.Requests.Transfer;

public record WithCashRequest(
    long AccountId,
    decimal Amount);