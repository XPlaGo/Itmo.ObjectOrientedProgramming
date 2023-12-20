using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdDto(long Id, string Username, UserRole Role, bool IsBlocked);