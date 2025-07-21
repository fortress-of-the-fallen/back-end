using Application.Feature.Auth.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Feature.Auth;

public static class AuthServiceInjection
{
    public static IServiceCollection AddAuthService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}