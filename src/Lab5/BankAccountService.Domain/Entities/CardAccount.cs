﻿namespace BankAccountService.Domain.Entities;

public class CardAccount : Account
{
    public CardAccount(
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

    public CardAccount(
        decimal amount,
        long userId,
        long currencyCode,
        DateTime createdDate,
        DateTime updatedDate)
    {
        Amount = amount;
        UserId = userId;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        CurrencyCode = currencyCode;
    }
}