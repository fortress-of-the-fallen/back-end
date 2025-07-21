using Application.Interface.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IdGenerator;

public static class IdGeneratorInjection
{
    public static IServiceCollection AddIdGenerator(this IServiceCollection services)
    {
        services.AddScoped<IIdGenerator, IdGenerator>();
        return services;
    }
}