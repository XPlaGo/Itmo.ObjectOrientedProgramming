namespace BankAccountService.Application.Features.Transfers.Commands.Transfer;

public class TransferCommand
{
    public long FromAccountId { get; set; }
    public long FromUserId { get; set; }
    public long ToAccountId { get; set; }
    public decimal Amount { get; set; }
}