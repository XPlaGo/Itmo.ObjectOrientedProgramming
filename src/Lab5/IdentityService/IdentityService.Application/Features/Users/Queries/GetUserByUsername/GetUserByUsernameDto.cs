using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetUserByUsername;

public record GetUserByUsernameDto(long Id, string Username, UserRole Role, bool IsBlocked);