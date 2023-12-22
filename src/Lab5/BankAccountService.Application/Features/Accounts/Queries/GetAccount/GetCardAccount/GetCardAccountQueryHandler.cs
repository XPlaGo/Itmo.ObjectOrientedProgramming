using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Accounts.Queries.GetAccount.GetCardAccount;

public class GetCardAccountQueryHandler : IRequestHandler<GetAccountRequest, Result<GetAccountResponse>>
{
    private readonly ICardAccountRepository _repository;

    public GetCardAccountQueryHandler(ICardAccountRepository repository)
    {
        _repository = repository;
    }

    public IRequestHandler<GetAccountRequest, Result<GetAccountResponse>>? Successor { get; set; }

    public async Task<Result<GetAccountResponse>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        CardAccount? account = await _repository
            .GetByIdAndUserIdAsync(request.AccountId, request.UserId)
            .ConfigureAwait(false);

        if (account is null)
        {
            if (Successor is null)
            {
                return await new ResultFactory()
                    .FailureAsync<GetAccountResponse>(
                        $"Accound with id {request.AccountId} for user with id {request.UserId} not found")
                    .ConfigureAwait(false);
            }

            return await Successor.Handle(request, cancellationToken).ConfigureAwait(false);
        }

        return await new ResultFactory()
            .SuccessAsync(new GetAccountResponse(
                account.Amount,
                account.CurrencyCode))
            .ConfigureAwait(false);
    }
}