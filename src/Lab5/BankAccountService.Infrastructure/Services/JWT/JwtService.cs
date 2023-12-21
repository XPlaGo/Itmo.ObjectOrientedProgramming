using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankAccountService.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BankAccountService.Infrastructure.Services.JWT;

public class JwtService : IAuthTokenService
{
    private readonly IOptions<AccessTokenSettings> _settings;

    public JwtService(IOptions<AccessTokenSettings> settings)
    {
        _settings = settings;
    }

    public Task<string> GenerateInternalAccessToken(string serviceName)
    {
        ArgumentNullException.ThrowIfNull(serviceName);

        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key)),
            algorithm: SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity();

        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, serviceName));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Internal"));

        var jwtHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = jwtHandler.CreateJwtSecurityToken(
            issuer: _settings.Value.Issuer,
            audience: _settings.Value.Audience,
            subject: claimsIdentity,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(_settings.Value.LifeTimeInSeconds),
            issuedAt: DateTime.UtcNow,
            signingCredentials: signingCredentials);

        string serializedJwt = jwtHandler.WriteToken(jwt);

        return Task.FromResult(serializedJwt);
    }
}