using System.Data;
using BankAccountService.Application.Exceptions;
using BankAccountService.Application.Interfaces.Factories;
using BankAccountService.Application.Interfaces.Repositories;
using BankAccountService.Application.Interfaces.Services;
using BankAccountService.Common;
using BankAccountService.Common.Factories;
using BankAccountService.Domain.Entities;

namespace BankAccountService.Application.Features.Accounts.Commands.CreateAccount.Savings;

public class CreateSavingsAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<CreateAccountResponse>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICurrencyConversionService _currencyConversionService;

    public CreateSavingsAccountCommandHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory, ICurrencyConversionService currencyConversionService)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _currencyConversionService = currencyConversionService;
    }

    public async Task<Result<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.RepeatableRead, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            Result<bool> currencyExists = await _currencyConversionService
                .CurrencyExists(request.CurrencyCode)
                .ConfigureAwait(false);

            if (currencyExists.Succeeded is false) throw new ServiceException(currencyExists.Messages);

            if (currencyExists.Data is false) throw new CurrencyNotFoundException(request.CurrencyCode);

            var savingsAccount = new SavingsAccount(
                0,
                request.UserId,
                request.CurrencyCode,
                DateTime.UtcNow,
                DateTime.UtcNow);

            long id = await unitOfWork.SavingsAccountRepository.AddAsync(savingsAccount).ConfigureAwait(false);

            await unitOfWork
                .CommitAsync(IsolationLevel.Serializable, cancellationToken)
                .ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(new CreateAccountResponse(id))
                .ConfigureAwait(false);
        }
        catch (DatabaseException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<CreateAccountResponse>(exception.Message).ConfigureAwait(false);
        }
        catch (ServiceException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<CreateAccountResponse>(exception.Messages).ConfigureAwait(false);
        }
        catch (CurrencyNotFoundException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<CreateAccountResponse>(exception.Message).ConfigureAwait(false);
        }
    }
}