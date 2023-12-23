using System.Data;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.Factories;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Auth.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<SignUpTokenDto>>
{
    private readonly IAuthTokenService _authTokenService;
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICryptographyService _cryptographyService;

    public SignUpCommandHandler(
        IAuthTokenService authTokenService,
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICryptographyService cryptographyService)
    {
        _authTokenService = authTokenService;
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _cryptographyService = cryptographyService;
    }

    public async Task<Result<SignUpTokenDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.RepeatableRead, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            User? userByUsername = await unitOfWork
                .UserRepository
                .GetUserByUsernameAsync(request.Username)
                .ConfigureAwait(false);

            if (userByUsername is not null) throw new UserAlreadyExistsException();

            string salt = _cryptographyService.GenerateSalt();

            var user = new User(
                request.Username,
                _cryptographyService.HashPassword(request.Password, salt),
                salt,
                false,
                UserRole.User,
                DateTime.UtcNow,
                DateTime.UtcNow);

            long userId = await unitOfWork
                .UserRepository
                .AddAsync(user)
                .ConfigureAwait(false);

            user.Id = userId;

            await unitOfWork
                .CommitAsync(IsolationLevel.Serializable, cancellationToken)
                .ConfigureAwait(false);

            string idToken = await _authTokenService.GenerateIdToken(user).ConfigureAwait(false);
            string accessToken = await _authTokenService.GenerateAccessToken(user).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(new SignUpTokenDto(idToken, accessToken))
                .ConfigureAwait(false);
        }
        catch (UserAlreadyExistsException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<SignUpTokenDto>(exception.Message).ConfigureAwait(false);
        }
    }
}