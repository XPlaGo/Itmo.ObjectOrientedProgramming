using IdentityService.Domain.Entities;

namespace IdentityService.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersDto(long Id, string Username, UserRole Role, bool IsBlocked);