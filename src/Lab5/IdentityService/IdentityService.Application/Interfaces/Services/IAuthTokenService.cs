using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces.Services;

public interface IAuthTokenService
{
    Task<string> GenerateIdToken(User user);
    Task<string> GenerateAccessToken(User user);
    Task<long> GetUserIdFromToken(string token);
    Task<bool> IsTokenValid(string accessToken, bool validateToken);
}