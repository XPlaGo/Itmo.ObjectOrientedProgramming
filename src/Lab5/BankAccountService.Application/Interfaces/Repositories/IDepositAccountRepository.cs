using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface IDepositAccountRepository : IAccountRepository<DepositAccount> { }