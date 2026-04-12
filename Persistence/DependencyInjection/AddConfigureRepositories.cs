using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Persistence.DependencyInjection;

internal static class AddConfigureRepositories
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services, bool isDev,
        bool enableVerboseLogs = false) // 🚩 Ver registro de repositorys
    {
        // Guardaremos los nombres en una lista local durante el escaneo 
        // para no tener que buscar luego en todo el IServiceCollection
        var registeredRepos = new List<string>();

        services.Scan(scan => scan
            .FromAssemblies(typeof(AddConfigureRepositories).Assembly)

            // Repositorios
            .AddClasses(c => c.InNamespaces("Persistence.Repositories").Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Queries
            .AddClasses(c => c.InNamespaces("Persistence.Queries").Where(t => t.Name.EndsWith("Query")))
                .AsSelf()
                .WithScopedLifetime()
        );

        // Optimizamos: Obtenemos solo los que pertenecen a nuestro namespace de Repositorios
        // Esto es mucho más rápido que filtrar el IServiceCollection completo
        if (isDev && enableVerboseLogs)
        {
            var repoNames = typeof(AddConfigureRepositories).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.Namespace == "Persistence.Repositories" &&
                        t.Name.EndsWith("Repository"))
            .Select(t => $"I{t.Name}") // Asumimos la convención IRepository
            .OrderBy(n => n)
            .ToList();

            PrintDetails(repoNames);
        }

        return services;
    }

    private static void PrintDetails(List<string> repos)
    {
        if (repos.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("    🧩 [DI] Repositorios registrados automáticamente:");
            foreach (var r in repos)
                Console.WriteLine($"       → {r}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ⚠️ [DI] No se detectaron repositorios en el ensamblado.");
            Console.ResetColor();
        }
    }
}