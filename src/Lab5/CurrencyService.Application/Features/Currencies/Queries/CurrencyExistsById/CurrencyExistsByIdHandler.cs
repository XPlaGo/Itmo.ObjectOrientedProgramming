using System.Data;
using CurrencyService.Application.Exceptions;
using CurrencyService.Application.Interfaces.Factories;
using CurrencyService.Application.Interfaces.Repositories;
using CurrencyService.Common;
using CurrencyService.Common.Factories;

namespace CurrencyService.Application.Features.Currencies.Queries.CurrencyExistsById;

public class CurrencyExistsByIdHandler : IRequestHandler<long, Result<bool>>
{
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;

    public CurrencyExistsByIdHandler(IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory)
    {
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
    }

    public async Task<Result<bool>> Handle(long request, CancellationToken cancellationToken)
    {
        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.RepeatableRead, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            _ = await unitOfWork
                    .CurrencyRepository
                    .GetByIdAsync(request)
                    .ConfigureAwait(false) ??
                throw new CurrencyNotFoundException(request);

            return await new ResultFactory()
                .SuccessAsync(true).ConfigureAwait(false);
        }
        catch (CurrencyNotFoundException)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory()
                .SuccessAsync(false).ConfigureAwait(false);
        }
    }
}