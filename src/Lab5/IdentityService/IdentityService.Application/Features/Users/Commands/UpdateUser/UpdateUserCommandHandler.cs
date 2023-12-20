using System.Data;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<long>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;

    public UpdateUserCommandHandler(IUnitOfWorkWithRepositories unitOfWorkWithRepositories)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
    }

    public async Task<Result<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        User? user = await _unitOfWorkWithRepositories.UserRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

        if (user is null) return await new ResultFactory().FailureAsync<long>("User Not Found").ConfigureAwait(false);

        user.Username = request.Username;
        user.Password = request.Password;
        user.IsBlocked = request.IsBlocked;

        await _unitOfWorkWithRepositories.UserRepository.UpdateAsync(user).ConfigureAwait(false);

        await _unitOfWorkWithRepositories.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

        return await new ResultFactory().SuccessAsync(user.Id, "User Updated").ConfigureAwait(false);
    }
}