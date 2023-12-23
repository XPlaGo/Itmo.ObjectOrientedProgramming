using AutoMapper;
using BankAccountService.Application.Features.Accounts.Commands.CreateAccount;
using BankAccountService.Models.Requests.Accounts;

namespace BankAccountService.Mapping;

public class AccountsMappingProfile : Profile
{
    public AccountsMappingProfile()
    {
        CreateMap<CreateAccountRequest, CreateAccountCommand>();
    }
}