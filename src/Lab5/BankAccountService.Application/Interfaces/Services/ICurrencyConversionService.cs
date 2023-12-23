using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Common;

namespace BankAccountService.Application.Interfaces.Services;

public interface ICurrencyConversionService
{
    public Task<Result<CurrencyConversionResponse>> Convert(CurrencyConversionRequest request);

    public Task<Result<bool>> CurrencyExists(long currencyCode);
}