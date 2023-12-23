using IdentityService.Application.Features.Auth.Commands.SignIn;
using IdentityService.Application.Features.Auth.Commands.SignUp;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddCommands();
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<SignInCommandHandler>();
        services.AddScoped<SignUpCommandHandler>();
    }
}