using IdentityService.Domain.Common;

namespace TransactionService.Domain.Entities;

public class Transaction : BaseMetaInfoEntity
{
    public Transaction() { }

    public Transaction(
        string transactionToken,
        long fromAccountId,
        string fromAccountType,
        long fromUserId,
        long fromCurrencyCode,
        decimal fromAmount,
        long toAccountId,
        string toAccountType,
        long toUserId,
        long toCurrencyCode,
        decimal toAmount,
        DateTime createdDate,
        DateTime updatedDate)
    {
        TransactionToken = transactionToken;
        FromAccountId = fromAccountId;
        FromAccountType = fromAccountType;
        FromUserId = fromUserId;
        FromCurrencyCode = fromCurrencyCode;
        FromAmount = fromAmount;
        ToAccountId = toAccountId;
        ToAccountType = toAccountType;
        ToUserId = toUserId;
        ToCurrencyCode = toCurrencyCode;
        ToAmount = toAmount;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

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