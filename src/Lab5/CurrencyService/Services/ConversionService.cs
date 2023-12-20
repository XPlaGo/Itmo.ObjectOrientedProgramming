﻿using CurrencyConversion;
using CurrencyService.Application.Features.CurrencyExchanges.Conversion;
using CurrencyService.Common;
using CurrencyService.Extensions;
using Grpc.Core;

namespace CurrencyService.Services;

public class ConversionService : ConversionServiceProto.ConversionServiceProtoBase
{
    private readonly ConversionCommandHandler _conversion;

    public ConversionService(ConversionCommandHandler conversion)
    {
        _conversion = conversion;
    }

    public override async Task<ConversionResultProto> Convert(ConversionRequestProto request, ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);

        var command = new ConversionCommand(
            request.FromCurrencyCode,
            request.ToCurrencyCode,
            request.Amount.ConvertToDecimal());
        Result<ConversionDto> result = await _conversion.Handle(command, default).ConfigureAwait(false);

        var resultProto = new ConversionResultProto
        {
            Success = result.Succeeded,
        };

        foreach (string message in result.Messages) resultProto.Messages.Add(message);

        var response = new ConversionResponseProto();

        if (result.Data is not null)
        {
            response.FromAmount = result.Data.FromAmount.ConvertToDecimalProto();
            response.ToAmount = result.Data.ToAmount.ConvertToDecimalProto();
        }

        resultProto.Data = response;

        return resultProto;
    }
}