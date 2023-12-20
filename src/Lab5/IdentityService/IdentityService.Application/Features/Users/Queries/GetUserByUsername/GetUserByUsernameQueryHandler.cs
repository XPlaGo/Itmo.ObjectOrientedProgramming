using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetUserByUsername;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, Result<GetUserByUsernameDto>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUnitOfWorkWithRepositories unitOfWorkWithRepositories, IUserRepository userRepository)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
        _userRepository = userRepository;
    }

    public async Task<Result<GetUserByUsernameDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        User user = await _userRepository.GetUserByUsernameAsync(request.Username).ConfigureAwait(false) ??
                    throw new UserNotFoundException();

        var userDto = new GetUserByUsernameDto(
            user.Id,
            user.Username,
            user.Role,
            user.IsBlocked);
        return await new ResultFactory().SuccessAsync(userDto).ConfigureAwait(false);
    }
}