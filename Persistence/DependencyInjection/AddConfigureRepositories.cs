using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces;

namespace Persistence.DependencyInjection;

internal static class AddConfigureRepositories
{
    /// <summary>
    /// Archivo para proceso de inyección de repositorios, parte de infraestructura.
    /// Por lo general deben ser de tipo AddScoped, debido a que DbContext es AddScoped.
    /// </summary>
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // 🔍 Escaneo automático de todos los *Repository en el ensamblado actual
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c
                .Where(t =>
                    t.Namespace != null &&
                    t.Namespace.Contains("Persistence.Repositories") &&
                    t.Name.EndsWith("Repository"))
            )
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        // Registrar queries
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c
                .Where(t => t.Namespace != null &&
                            t.Namespace.Contains("Persistence.Queries") &&
                            t.Name.EndsWith("Query"))
            )
            .AsSelf() // queries normalmente no implementan interfaces
            .WithScopedLifetime()
        );

        ValidateRepositories(services);

        return services;
    }

    /// <summary>
    /// Validación opcional que muestra advertencias si algún repositorio no tiene su implementación registrada.
    /// </summary>
    private static void ValidateRepositories(IServiceCollection services)
    {
        var registered = services
            .Where(s => s.ServiceType.FullName?.Contains("Repository") == true)
            .Select(s => s.ServiceType.Name)
            .ToHashSet();

        Console.WriteLine("🧩 [DI] Repositorios registrados automáticamente:");
        foreach (var r in registered)
            Console.WriteLine($"   → {r}");

        if (registered.Count == 0)
            Console.WriteLine("⚠️ No se detectaron repositorios. Verifica los namespaces o nombres.");
    }
}