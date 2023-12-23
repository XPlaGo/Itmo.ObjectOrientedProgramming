using System.Data;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.Factories;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Auth.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInTokenDto>>
{
    private readonly IAuthTokenService _authTokenService;
    private readonly IUnitOfWorkWithRepositoriesFactory _unitOfWorkWithRepositoriesFactory;
    private readonly ICryptographyService _cryptographyService;

    public SignInCommandHandler(
        IAuthTokenService authTokenService,
        IUnitOfWorkWithRepositoriesFactory unitOfWorkWithRepositoriesFactory,
        ICryptographyService cryptographyService)
    {
        _authTokenService = authTokenService;
        _unitOfWorkWithRepositoriesFactory = unitOfWorkWithRepositoriesFactory;
        _cryptographyService = cryptographyService;
    }

    public async Task<Result<SignInTokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using IUnitOfWorkWithRepositories unitOfWork = await _unitOfWorkWithRepositoriesFactory
            .Create(IsolationLevel.RepeatableRead, cancellationToken)
            .ConfigureAwait(false);

        try
        {
            User? user = await unitOfWork
                .UserRepository
                .GetUserByUsernameAsync(request.Username)
                .ConfigureAwait(false);

            if (user is null) throw new UserNotFoundException();

            if (!CheckCredentials(request.Password, user))
            {
                return await new ResultFactory()
                    .FailureAsync<SignInTokenDto>("Password is incorrect")
                    .ConfigureAwait(false);
            }

            string idToken = await _authTokenService.GenerateIdToken(user).ConfigureAwait(false);
            string accessToken = await _authTokenService.GenerateAccessToken(user).ConfigureAwait(false);

            return await new ResultFactory()
                .SuccessAsync(new SignInTokenDto(idToken, accessToken))
                .ConfigureAwait(false);
        }
        catch (UserNotFoundException exception)
        {
            await unitOfWork.RollbackAsync(cancellationToken).ConfigureAwait(false);
            return await new ResultFactory().FailureAsync<SignInTokenDto>(exception.Message).ConfigureAwait(false);
        }
    }

    private bool CheckCredentials(string password, User user)
    {
        string hash = _cryptographyService.HashPassword(password, user.Salt);
        return hash == user.Password;
    }
}