using Microsoft.Extensions.DependencyInjection;
using Application.Feature.Auth.Commands;

namespace Infrastructure.MediatR;

public static class MediatRInjection
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GithubLoginCommandHandler).Assembly));
    }
}