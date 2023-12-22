namespace BankAccountService.Models.Requests.Transfer;

public record TransferRequest(
    long FromAccountId,
    long ToAccountId,
    decimal Amount);