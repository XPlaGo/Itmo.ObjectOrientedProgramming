using AutoMapper;
using CurrencyConversion;
using CurrencyService.Application.Features.CurrencyExchanges.Conversion;
using CurrencyService.Extensions;

namespace CurrencyService.Mapping;

public class ConversionMapperProfile : Profile
{
    public ConversionMapperProfile()
    {
        CreateMap<DecimalProto, decimal>().ConvertUsing(decimalProto => decimalProto.ConvertToDecimal());
        CreateMap<decimal, DecimalProto>().ConvertUsing(systemDecimal => systemDecimal.ConvertToDecimalProto());
        CreateMap<ConversionRequestProto, ConversionCommand>();
        CreateMap<ConversionDto, ConversionResponseProto>();
    }
}