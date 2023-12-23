namespace TransactionService.Application.Features.Transactions.Queries.GetAllByUserId;

public record GetAllByUserIdResponse(
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
    decimal ToAmount,
    DateTime CreatedDate,
    DateTime UpdatedDate);