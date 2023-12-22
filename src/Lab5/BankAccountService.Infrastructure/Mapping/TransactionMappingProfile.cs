using AutoMapper;
using BankAccountService.Application.Models.CurrencyConversion;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Infrastructure.Extensions;
using CurrencyConversion;
using Transaction;
using DecimalProto = Transaction.DecimalProto;

namespace BankAccountService.Infrastructure.Mapping;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<DecimalProto, decimal>().ConvertUsing(decimalProto => decimalProto.ConvertToDecimal());
        CreateMap<decimal, DecimalProto>().ConvertUsing(systemDecimal => systemDecimal.ConvertToTransactionDecimalProto());

        CreateMap<CreateTransactionRequest, TransactionRequestProto>();
        CreateMap<UpdateTransactionRequest, TransactionRequestProto>();
        CreateMap<DeleteTransactionRequest, TransactionTokenRequestProto>();
        CreateMap<ConversionResponseProto, CurrencyConversionResponse>();
    }
}