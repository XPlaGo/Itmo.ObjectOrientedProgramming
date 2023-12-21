using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyService.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(environment);

        IConfigurationSection jwtSettingsConfiguration = configuration.GetSection("AccessTokenSettings");
        services.Configure<AccessTokenSettings>(jwtSettingsConfiguration);
        AccessTokenSettings jwtSettings = jwtSettingsConfiguration.Get<AccessTokenSettings>() ??
                                           throw new InvalidOperationException();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                if (environment.IsDevelopment()) options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.FromMinutes(0),
                };
            });

        services.AddAuthorization();
    }
}