using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Interfaces.Repositories;

public interface ISavingsAccountRepository : IAccountRepository<SavingsAccount> { }