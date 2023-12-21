namespace BankAccountService.Domain.Entities;

public class SavingsAccount : Account
{
    public SavingsAccount(
        long id,
        decimal amount,
        long userId,
        long currencyCode,
        DateTime createdDate,
        DateTime updatedDate)
    {
        Id = id;
        Amount = amount;
        UserId = userId;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        CurrencyCode = currencyCode;
    }
}