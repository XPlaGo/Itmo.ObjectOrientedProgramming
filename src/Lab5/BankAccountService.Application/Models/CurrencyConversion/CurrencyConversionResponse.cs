namespace BankAccountService.Application.Models.CurrencyConversion;

public record CurrencyConversionResponse(
    decimal FromAmount,
    decimal ToAmount);