using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Exceptions;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Services.JWT;

public class JwtService : IAuthTokenService
{
    private readonly IOptions<AccessTokenSettings> _settings;

    public JwtService(IOptions<AccessTokenSettings> settings)
    {
        _settings = settings;
    }

    public Task<string> GenerateIdToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key)),
            algorithm: SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity();

        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(culture)));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

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

    public Task<string> GenerateAccessToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var signingCredentials = new SigningCredentials(
            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key)),
            algorithm: SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity();

        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        culture = (CultureInfo)culture.Clone();
        Thread.CurrentThread.CurrentCulture = culture;

        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(culture)));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

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

    public Task<long> GetUserIdFromToken(string token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                ValidIssuer = _settings.Value.Issuer,
                ValidAudience = _settings.Value.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key)),
                ClockSkew = TimeSpan.FromMinutes(0),
            };

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            culture = (CultureInfo)culture.Clone();
            Thread.CurrentThread.CurrentCulture = culture;

            var jwtHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal? claims = jwtHandler.ValidateToken(token, tokenValidationParameters, out _);
            long userId =
                long.Parse(
                    claims
                        .FindFirst(ClaimTypes.NameIdentifier)?
                        .Value ?? throw new InvalidOperationException(),
                    culture);

            return Task.FromResult(userId);
        }
        catch (Exception exception)
        {
            throw new InvalidTokenException(exception.Message, exception);
        }
    }

    public Task<bool> IsTokenValid(string accessToken, bool validateToken)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateToken,
                ValidateIssuerSigningKey = false,
                ValidIssuer = _settings.Value.Issuer,
                ValidAudience = _settings.Value.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key)),
                ClockSkew = TimeSpan.FromMinutes(0),
            };

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            culture = (CultureInfo)culture.Clone();
            Thread.CurrentThread.CurrentCulture = culture;

            var jwtHandler = new JwtSecurityTokenHandler();
            jwtHandler.ValidateToken(accessToken, tokenValidationParameters, out _);

            return Task.FromResult(true);
        }
#pragma warning disable CA1031
        catch (Exception)
#pragma warning restore CA1031
        {
            return Task.FromResult(false);
        }
    }
}