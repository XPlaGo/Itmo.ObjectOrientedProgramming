namespace CurrencyService.Application.Features.CurrencyExchanges.Conversion;

public record ConversionDto(
    decimal FromAmount,
    decimal ToAmount);