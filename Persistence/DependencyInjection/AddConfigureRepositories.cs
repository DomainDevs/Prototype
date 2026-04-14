using Microsoft.Extensions.DependencyInjection;

namespace Persistence.DependencyInjection;

internal static class AddConfigureRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(AddConfigureRepositories).Assembly;

        services.Scan(scan => scan
            .FromAssemblies(assembly)

            .AddClasses(c => c.InNamespaces("Persistence.Repositories")
                .Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(c => c.InNamespaces("Persistence.Queries")
                .Where(t => t.Name.EndsWith("Query")))
                .AsSelf()
                .WithScopedLifetime()
        );

        return services;
    }
}