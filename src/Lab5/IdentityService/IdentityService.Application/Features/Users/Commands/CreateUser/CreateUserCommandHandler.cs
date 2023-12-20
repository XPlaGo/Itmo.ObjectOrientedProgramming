using System.Data;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<long>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;
    private readonly ICryptographyService _cryptographyService;

    public CreateUserCommandHandler(
        IUnitOfWorkWithRepositories unitOfWorkWithRepositories,
        ICryptographyService cryptographyService)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
        _cryptographyService = cryptographyService;
    }

    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(cancellationToken);

        string salt = _cryptographyService.GenerateSalt();

        var user = new User(
            request.Username,
            request.Password,
            salt,
            request.IsBlocked,
            request.Role,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _unitOfWorkWithRepositories.UserRepository.AddAsync(user).ConfigureAwait(false);

        await _unitOfWorkWithRepositories.CommitAsync(IsolationLevel.Serializable, cancellationToken).ConfigureAwait(false);

        return await new ResultFactory().SuccessAsync(user.Id, "User Created").ConfigureAwait(false);
    }
}