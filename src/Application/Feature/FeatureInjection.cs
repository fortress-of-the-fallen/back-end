using Application.Feature.Auth.Commands;
using Application.Feature.Auth.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Feature.Auth;

public static class FeatureInjection
{
    public static void AddFeatureInjection(this IServiceCollection services)
    {
        services.AddAuthFeature();
    }
    
    private static void AddAuthFeature(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}