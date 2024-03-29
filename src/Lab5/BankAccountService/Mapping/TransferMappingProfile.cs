﻿using AutoMapper;
using BankAccountService.Application.Features.Transfers.Commands.Transfer;
using BankAccountService.Application.Features.Transfers.Commands.Transfer.WithCash;
using BankAccountService.Models.Requests.Transfer;

namespace BankAccountService.Mapping;

public class TransferMappingProfile : Profile
{
    public TransferMappingProfile()
    {
        CreateMap<TransferRequest, TransferCommand>();
        CreateMap<WithCashRequest, WithCashCardAccountCommand>();
    }
}