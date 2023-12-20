using IdentityService.Application.Features.Auth.Commands.SignIn;
using IdentityService.Application.Features.Auth.Commands.SignUp;
using IdentityService.Application.Features.Users.Commands.CreateUser;
using IdentityService.Application.Features.Users.Commands.DeleteUser;
using IdentityService.Application.Features.Users.Commands.UpdateUser;
using IdentityService.Application.Features.Users.Queries.GetAllUsers;
using IdentityService.Application.Features.Users.Queries.GetUserById;
using IdentityService.Application.Features.Users.Queries.GetUserByUsername;
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
        services.AddScoped<CreateUserCommandHandler>();
        services.AddScoped<DeleteUserCommandHandler>();
        services.AddScoped<UpdateUserCommandHandler>();
        services.AddScoped<GetAllUsersQueryHandler>();
        services.AddScoped<GetUserByIdQueryHandler>();
        services.AddScoped<GetUserByUsernameQueryHandler>();
    }
}