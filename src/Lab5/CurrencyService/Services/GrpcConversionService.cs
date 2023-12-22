using AutoMapper;
using CurrencyConversion;
using CurrencyService.Application.Features.CurrencyExchanges.Conversion;
using CurrencyService.Common;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace CurrencyService.Services;

public class GrpcConversionService : ConversionServiceProto.ConversionServiceProtoBase
{
    private readonly ConversionCommandHandler _conversion;
    private readonly IMapper _mapper;

    public GrpcConversionService(ConversionCommandHandler conversion, IMapper mapper)
    {
        _conversion = conversion;
        _mapper = mapper;
    }

    [Authorize(Roles = "Internal,Admin")]
    public override async Task<ConversionResultProto> Convert(ConversionRequestProto request, ServerCallContext context)
    {
        ArgumentNullException.ThrowIfNull(request);

        ConversionCommand command = _mapper.Map<ConversionRequestProto, ConversionCommand>(request);

        Result<ConversionDto> result = await _conversion.Handle(command, default).ConfigureAwait(false);

        var resultProto = new ConversionResultProto
        {
            Succeeded = result.Succeeded,
            Data = result.Data is null ? null : _mapper.Map<ConversionDto, ConversionResponseProto>(result.Data),
        };

        foreach (string message in result.Messages) resultProto.Messages.Add(message);

        return resultProto;
    }
}