using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Common;
using BankAccountService.Infrastructure.Extensions;
using CurrencyConversion;
using Grpc.Net.Client;
using IdentityService.Extensions;

namespace BankAccountService.Infrastructure.Services.CurrencyConversion;

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly string _grpcServerHost;

    public CurrencyConversionService(string grpcServerHost)
    {
        _grpcServerHost = grpcServerHost;
    }

    public async Task<Result<CurrencyConversionResponse>> Convert(CurrencyConversionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        using var channel = GrpcChannel.ForAddress(_grpcServerHost);
        var client = new ConversionServiceProto.ConversionServiceProtoClient(channel);

        var conversionRequest = new ConversionRequestProto
        {
            FromCurrencyCode = request.FromCurrencyCode,
            ToCurrencyCode = request.ToCurrencyCode,
            Amount = request.Amount.ConvertToDecimalProto(),
        };

        ConversionResultProto response = await client.ConvertAsync(conversionRequest).ConfigureAwait(false);

        CurrencyConversionResponse? data = response.Data?.FromAmount is null || response.Data?.ToAmount is null
            ? null
            : new CurrencyConversionResponse(
                response.Data.FromAmount.ConvertToDecimal(),
                response.Data.ToAmount.ConvertToDecimal());

        return new Result<CurrencyConversionResponse>(
            response.Messages.ToList(),
            response.Success,
            data);
    }
}