using AutoMapper;
using Transaction;
using TransactionService.Application.Features.Transactions.Commands.CreateTransaction;
using TransactionService.Application.Features.Transactions.Commands.DeleteTransaction;
using TransactionService.Application.Features.Transactions.Commands.UpdateTransaction;
using TransactionService.Extensions;

namespace TransactionService.Mapping;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<DecimalProto, decimal>().ConvertUsing(decimalProto => decimalProto.ConvertToDecimal());
        CreateMap<decimal, DecimalProto>().ConvertUsing(systemDecimal => systemDecimal.ConvertToDecimalProto());
        CreateMap<TransactionRequestProto, CreateTransactionCommand>();
        CreateMap<TransactionRequestProto, UpdateTransactionCommand>();
        CreateMap<TransactionTokenRequestProto, DeleteTransactionCommand>();
    }
}