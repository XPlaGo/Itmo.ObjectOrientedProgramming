using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<GetAllUsersDto>>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;

    public GetAllUsersQueryHandler(IUnitOfWorkWithRepositories unitOfWorkWithRepositories)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
    }

    public async Task<Result<IReadOnlyList<GetAllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<GetAllUsersDto> users = (await _unitOfWorkWithRepositories
                .UserRepository
                .GetAllAsync()
                .ConfigureAwait(false))
            .Select<User, GetAllUsersDto>(user => new GetAllUsersDto(
                user.Id,
                user.Username,
                user.Role,
                user.IsBlocked)).ToList();

        return await new ResultFactory().SuccessAsync(users).ConfigureAwait(false);
    }
}