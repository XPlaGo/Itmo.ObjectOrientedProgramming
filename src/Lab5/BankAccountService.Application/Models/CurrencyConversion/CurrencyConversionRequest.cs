namespace BankAccountService.Application.Models.CurrencyConversion;

public record CurrencyConversionRequest(long FromCurrencyCode, long ToCurrencyCode, decimal Amount);