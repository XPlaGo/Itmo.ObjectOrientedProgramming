using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Common;
using IdentityService.Common.Factories;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdDto>>
{
    private readonly IUnitOfWorkWithRepositories _unitOfWorkWithRepositories;

    public GetUserByIdQueryHandler(IUnitOfWorkWithRepositories unitOfWorkWithRepositories)
    {
        _unitOfWorkWithRepositories = unitOfWorkWithRepositories;
    }

    public async Task<Result<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        User? user = await _unitOfWorkWithRepositories.UserRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

        if (user is null) return await new ResultFactory().FailureAsync<GetUserByIdDto>("User Not Found").ConfigureAwait(false);

        var userDto = new GetUserByIdDto(user.Id, user.Username, user.Role, user.IsBlocked);
        return await new ResultFactory().SuccessAsync(userDto).ConfigureAwait(false);
    }
}