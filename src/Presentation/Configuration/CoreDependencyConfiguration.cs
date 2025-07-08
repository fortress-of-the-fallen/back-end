namespace FortressOfTheFallen.Presentation.Configuration;

public static class CoreDependencyConfiguration
{
    public static void AddCoreDependencies(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddHttpContextAccessor();
    }
}