using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess;

public static class DataAccessInjection
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddUoW();
    }
}