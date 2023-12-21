using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Infrastructure.Extensions;
using BankAccountService.Infrastructure.Services.JWT;
using CurrencyConversion;
using Grpc.Core;
using Grpc.Net.Client;

namespace BankAccountService.Infrastructure.Services.CurrencyConversion;

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly GrpcServicesSettings _settings;
    private readonly IAuthTokenService _tokenService;

    public CurrencyConversionService(GrpcServicesSettings settings, IAuthTokenService tokenService)
    {
        _settings = settings;
        _tokenService = tokenService;
    }

    public async Task<Result<CurrencyConversionResponse>> Convert(CurrencyConversionRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request);

            string jwtToken =
                await _tokenService.GenerateInternalAccessToken("BankAccountService").ConfigureAwait(false);

            using var channel = GrpcChannel.ForAddress(_settings.CurrencyServiceAddress);
            var client = new ConversionServiceProto.ConversionServiceProtoClient(channel);

            var conversionRequest = new ConversionRequestProto
            {
                FromCurrencyCode = request.FromCurrencyCode,
                ToCurrencyCode = request.ToCurrencyCode,
                Amount = request.Amount.ConvertToCurrencyDecimalProto(),
            };

            var headers = new Metadata { { "Authorization", $"Bearer {jwtToken}" } };

            ConversionResultProto response =
                await client.ConvertAsync(conversionRequest, headers).ConfigureAwait(false);

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
        catch (RpcException exception)
        {
            return await new ResultFactory()
                .FailureAsync<CurrencyConversionResponse>(exception.Message)
                .ConfigureAwait(false);
        }
    }
}