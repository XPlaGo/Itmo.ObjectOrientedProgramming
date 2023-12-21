using BankAccountService.Application.Models.Transaction;
using BankAccountService.Common;

namespace BankAccountService.Application.Interfaces.Services;

public interface ITransactionService
{
    public Task<Result<string>> Create(CreateTransactionRequest request);
    public Task<Result<string>> Delete(DeleteTransactionRequest request);
    public Task<Result<string>> Update(UpdateTransactionRequest request);
}