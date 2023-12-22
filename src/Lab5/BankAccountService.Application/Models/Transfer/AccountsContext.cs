using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Models.Transfer;

public record AccountsContext<TFromAccount, TToAccount>(TFromAccount FromAccount, TToAccount ToAccount)
    where TFromAccount : Account
    where TToAccount : Account;