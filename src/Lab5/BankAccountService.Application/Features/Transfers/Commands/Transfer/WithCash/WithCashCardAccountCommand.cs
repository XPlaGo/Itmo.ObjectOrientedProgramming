namespace BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash;

public class WithCashCardAccountCommand
{
    public long AccountId { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
}