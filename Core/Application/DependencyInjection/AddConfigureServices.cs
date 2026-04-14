using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class AddConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var assembly = typeof(AddConfigureServices).Assembly;

        services.Scan(scan => scan
            .FromAssemblies(assembly)

            .AddClasses(c => c.InNamespaces("Application.Features")
                .Where(t => t.Namespace != null && t.Namespace.Contains(".Services")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            .AddClasses(c => c.InNamespaces("Application.UseCase")
                .Where(t => t.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithScopedLifetime()
        );

        return services;
    }
}