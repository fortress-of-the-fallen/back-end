using Infrastructure.DataAccess.DbContext;
using Infrastructure.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.UnitOfWork;

public static class UoWInjection
{
    public static void AddUoW(this IServiceCollection services)
    {
        services.AddScoped<ReadUnitOfWork>(sp => new ReadUnitOfWork(sp.GetRequiredService<BaseDbContext>()));
        services.AddScoped<WriteUnitOfWork>(sp => new WriteUnitOfWork(sp.GetRequiredService<BaseDbContext>()));
    }
}