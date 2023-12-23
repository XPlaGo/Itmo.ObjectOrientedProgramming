using IdentityService.Application.Interfaces.Services;
using IdentityService.Infrastructure.Services.Cryptography;
using IdentityService.Infrastructure.Services.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLevel(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddServices(configuration);
    }

    private static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection accessTokenConfiguration = configuration.GetSection("AccessTokenSettings");
        services.Configure<AccessTokenSettings>(accessTokenConfiguration);
        services.AddSingleton<IAuthTokenService, JwtService>();
        services.AddSingleton<ICryptographyService, CryptographyService>();
    }
}