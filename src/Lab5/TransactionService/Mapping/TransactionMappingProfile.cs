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
        CreateMap<TransactionRequestProto, CreateTransactionCommand>()
            .ForMember(
                dest => dest.FromAmount,
                opt => opt.MapFrom(src => src.FromAmount.ConvertToDecimal()))
            .ForMember(
                dest => dest.ToAmount,
                opt => opt.MapFrom(src => src.ToAmount.ConvertToDecimal()));
        CreateMap<TransactionRequestProto, UpdateTransactionCommand>()
            .ForMember(
                dest => dest.FromAmount,
                opt => opt.MapFrom(src => src.FromAmount.ConvertToDecimal()))
            .ForMember(
                dest => dest.ToAmount,
                opt => opt.MapFrom(src => src.ToAmount.ConvertToDecimal()));
        CreateMap<TransactionTokenRequestProto, DeleteTransactionCommand>();
    }
}