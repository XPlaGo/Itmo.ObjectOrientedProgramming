using AutoMapper;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Infrastructure.Extensions;
using CurrencyConversion;

namespace BankAccountService.Infrastructure.Mapping;

public class CurrencyMappingProfile : Profile
{
    public CurrencyMappingProfile()
    {
        CreateMap<DecimalProto, decimal>().ConvertUsing(decimalProto => decimalProto.ConvertToDecimal());
        CreateMap<decimal, DecimalProto>().ConvertUsing(systemDecimal => systemDecimal.ConvertToCurrencyDecimalProto());

        CreateMap<CurrencyConversionRequest, ConversionRequestProto>();
    }
}