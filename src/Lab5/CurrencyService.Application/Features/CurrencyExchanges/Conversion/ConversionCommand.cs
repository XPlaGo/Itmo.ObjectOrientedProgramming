using System.ComponentModel.DataAnnotations;

namespace CurrencyService.Application.Features.CurrencyExchanges.Conversion;

public record ConversionCommand(
    [Required(ErrorMessage = "From currency Id is required")]
    long FromCurrencyCode,
    [Required(ErrorMessage = "To currency Id is required")]
    long ToCurrencyCode,
    [Required(ErrorMessage = "Amount is required")]
    decimal Amount);