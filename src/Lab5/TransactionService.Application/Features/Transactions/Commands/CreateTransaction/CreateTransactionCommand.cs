namespace TransactionService.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand
{
    public string TransactionToken { get; set; } = string.Empty;
    public long FromAccountId { get; set; }
    public string FromAccountType { get; set; } = string.Empty;
    public long FromUserId { get; set; }
    public long FromCurrencyCode { get; set; }
    public decimal FromAmount { get; set; }
    public long ToAccountId { get; set; }
    public string ToAccountType { get; set; } = string.Empty;
    public long ToUserId { get; set; }
    public long ToCurrencyCode { get; set; }
    public decimal ToAmount { get; set; }
}