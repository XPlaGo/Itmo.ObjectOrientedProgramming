namespace BankAccountService.Application.Features.Transfers.Commands.Transfer;

public record TransferResponse(
    decimal TransferredFromAmount,
    decimal TransferredToAmount);