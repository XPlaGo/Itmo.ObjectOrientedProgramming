namespace TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand
{
    public string TransactionToken { get; set; } = string.Empty;
}