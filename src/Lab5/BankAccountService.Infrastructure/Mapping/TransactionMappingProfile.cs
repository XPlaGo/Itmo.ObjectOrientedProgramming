using AutoMapper;
using BankAccountService.Application.Models.Transaction;
using BankAccountService.Infrastructure.Extensions;
using Transaction;

namespace BankAccountService.Infrastructure.Mapping;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<CreateTransactionRequest, TransactionRequestProto>()
            .ForMember(
                dest => dest.FromAmount,
                opt => opt.MapFrom(src => src.FromAmount.ConvertToTransactionDecimalProto()))
            .ForMember(
                dest => dest.ToAmount,
                opt => opt.MapFrom(src => src.ToAmount.ConvertToTransactionDecimalProto()));
        CreateMap<UpdateTransactionRequest, TransactionRequestProto>()
            .ForMember(
                dest => dest.FromAmount,
                opt => opt.MapFrom(src => src.FromAmount.ConvertToTransactionDecimalProto()))
            .ForMember(
                dest => dest.ToAmount,
                opt => opt.MapFrom(src => src.ToAmount.ConvertToTransactionDecimalProto()));
        CreateMap<DeleteTransactionRequest, TransactionTokenRequestProto>();
    }
}