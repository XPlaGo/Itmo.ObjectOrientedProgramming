namespace BankAccountService.Application.Models.Transaction;

public record CreateTransactionRequest(
    string TransactionToken,
    long FromAccountId,
    string FromAccountType,
    long FromUserId,
    long FromCurrencyCode,
    decimal FromAmount,
    long ToAccountId,
    string ToAccountType,
    long ToUserId,
    long ToCurrencyCode,
    decimal ToAmount);