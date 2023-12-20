namespace BankAccountService.Application.Features.Transfers.Commands.Transfer;

public record TransferCommand(
    long FromAccountId,
    long FromUserId,
    long ToAccountId,
    long ToUserId,
    decimal Amount);