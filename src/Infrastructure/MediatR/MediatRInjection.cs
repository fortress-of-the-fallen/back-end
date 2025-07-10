using Microsoft.Extensions.DependencyInjection;
using Application.Feature.Samples.Command;

namespace Infrastructure.MediatR;

public static class MediatRInjection
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSampleCommandHandler).Assembly));
    }
}