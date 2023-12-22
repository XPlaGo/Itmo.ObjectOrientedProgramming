using AutoMapper;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Infrastructure.Services.JWT;
using CurrencyConversion;
using Grpc.Core;
using Grpc.Net.Client;

namespace BankAccountService.Infrastructure.Services.CurrencyConversion;

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly GrpcServicesSettings _settings;
    private readonly IAuthTokenService _tokenService;
    private readonly IMapper _mapper;

    public CurrencyConversionService(GrpcServicesSettings settings, IAuthTokenService tokenService, IMapper mapper)
    {
        _settings = settings;
        _tokenService = tokenService;
        _mapper = mapper;
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

            ConversionRequestProto conversionRequest =
                _mapper.Map<CurrencyConversionRequest, ConversionRequestProto>(request);

            var headers = new Metadata { { "Authorization", $"Bearer {jwtToken}" } };

            ConversionResultProto result =
                await client.ConvertAsync(conversionRequest, headers).ConfigureAwait(false);

            if (result.Succeeded is false)
            {
                return await new ResultFactory()
                    .FailureAsync<CurrencyConversionResponse>(result.Messages.ToList())
                    .ConfigureAwait(false);
            }

            CurrencyConversionResponse data =
                _mapper.Map<ConversionResponseProto, CurrencyConversionResponse>(result.Data);

            return await new ResultFactory()
                .SuccessAsync(data)
                .ConfigureAwait(false);
        }
        catch (RpcException exception)
        {
            return await new ResultFactory()
                .FailureAsync<CurrencyConversionResponse>(exception.Message)
                .ConfigureAwait(false);
        }
    }
}