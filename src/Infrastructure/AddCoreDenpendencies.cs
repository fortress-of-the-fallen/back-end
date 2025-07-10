using Infrastructure.Caching;
using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class AddCoreDenpendencies
{
    public static void AddDenpendencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRedis(configuration);
        services.AddDbContext(configuration);
        services.AddUoW();
    }
}