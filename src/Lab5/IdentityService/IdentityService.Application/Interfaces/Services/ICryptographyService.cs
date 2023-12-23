namespace IdentityService.Application.Interfaces.Services;

public interface ICryptographyService
{
    public string GenerateSalt();
    public string HashPassword(string password, string salt);
}