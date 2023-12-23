using System.Security.Cryptography;
using System.Text;
using IdentityService.Application.Interfaces.Services;
using Konscious.Security.Cryptography;

namespace IdentityService.Infrastructure.Services.Cryptography;

public class CryptographyService : ICryptographyService
{
    private const int _hashSize = 64;

    public CryptographyService() { }

    [Obsolete("Obsolete")]
    public string GenerateSalt()
    {
        byte[] buffer = new byte[_hashSize];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }

    public string HashPassword(string password, string salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

#pragma warning disable CA2000
        var argon = new Argon2i(passwordBytes)
        {
            DegreeOfParallelism = 16,
            MemorySize = 8192,
            Iterations = 40,
            Salt = saltBytes,
        };
#pragma warning restore CA2000

        byte[] hash = argon.GetBytes(_hashSize);
        return Convert.ToBase64String(hash);
    }
}