using System.Data;
using CurrencyService.Application.Exceptions;
using CurrencyService.Application.Interfaces.Factories;
using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Common;
using CurrencyService.Common.Factories;
using CurrencyService.Domain.Entities;

namespace CurrencyService.Application.Features.CurrencyExchanges.Conversion;

public class ConversionCommandHandler : IRequestHandler<ConversionCommand, Result<ConversionDto>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public ConversionCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<ConversionDto>> Handle(ConversionCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.Serializable, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            Currency fromCurrency = await unitOfWork
                                        .CurrencyRepository
                                        .GetByIdAsync(request.FromCurrencyCode)
                                        .ConfigureAwait(false) ??
                                    throw new CurrencyNotFoundException(request.FromCurrencyCode);

            Currency toCurrency = await unitOfWork
                                      .CurrencyRepository
                                      .GetByIdAsync(request.ToCurrencyCode)
                                      .ConfigureAwait(false) ??
                                  throw new CurrencyNotFoundException(request.ToCurrencyCode);

            CurrencyExchange currencyExchange = await unitOfWork
                                                    .CurrencyExchangeRepository
                                                    .GetByCodesAsync(
                                                        fromCurrency.CurrencyCode,
                                                        toCurrency.CurrencyCode)
                                                    .ConfigureAwait(false) ??
                                                throw new CannotExchangeCurrenciesException(
                                                    fromCurrency.CurrencyCode,
                                                    toCurrency.CurrencyCode,
                                                    "exchange is not supported");

            decimal fromAmount = request.Amount;
            decimal toAmount = fromAmount * currencyExchange.ExchangeRate;

            return await new ResultFactory()
                .SuccessAsync(new ConversionDto(fromAmount, toAmount))
                .ConfigureAwait(false);
        }
        catch (CurrencyNotFoundException exception)
        {
            return await new ResultFactory().FailureAsync<ConversionDto>(exception.Message).ConfigureAwait(false);
        }
        catch (CannotExchangeCurrenciesException exception)
        {
            return await new ResultFactory().FailureAsync<ConversionDto>(exception.Message).ConfigureAwait(false);
        }
    }
}