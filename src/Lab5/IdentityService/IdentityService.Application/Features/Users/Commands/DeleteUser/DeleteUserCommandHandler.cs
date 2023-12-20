using System.Data;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<long>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;

    public DeleteUserCommandHandler(IUnitOfWorkWithRepositories unitOfWorkWithRepositories)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
    }

    public async Task<Result<long>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        User? user = await _unitOfWorkWithRepositories.UserRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

        if (user is null) return await new ResultFactory().FailureAsync<long>("User Not Found").ConfigureAwait(false);

        await _unitOfWorkWithRepositories.UserRepository.DeleteAsync(user).ConfigureAwait(false);

        await _unitOfWorkWithRepositories.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

        return await new ResultFactory().SuccessAsync(user.Id, "User Deleted").ConfigureAwait(false);
    }
}