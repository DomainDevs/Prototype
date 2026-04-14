using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Persistence.DependencyInjection;

internal static class AddConfigureRepositories
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        bool enableVerboseLogs = false)
    {
        var assembly = typeof(AddConfigureRepositories).Assembly;

        // =========================
        // DI REGISTRATION
        // =========================
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

        // =========================
        // DIAGNOSTICS
        // =========================
        if (enableVerboseLogs)
        {
            var model = RepositoryDiagnosticsEngine.Build(assembly);
            RepositoryDiagnosticsRenderer.Render(model);
        }

        return services;
    }

    // =====================================================
    // ENGINE (PURE LOGIC)
    // =====================================================
    private static class RepositoryDiagnosticsEngine
    {
        public static RepositoryDiagnosticsModel Build(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToArray();

            var repos = types
                .Where(t =>
                    t.Namespace == "Persistence.Repositories" &&
                    t.Name.EndsWith("Repository"))
                .Select(t => new RepositoryInfo(
                    Interface: $"I{t.Name}",
                    Implementation: t.Name))
                .OrderBy(x => x.Interface)
                .ToList();

            return new RepositoryDiagnosticsModel(repos);
        }
    }

    // =====================================================
    // RENDERER (OUTPUT LAYER ONLY)
    // =====================================================
    private static class RepositoryDiagnosticsRenderer
    {
        public static void Render(RepositoryDiagnosticsModel model)
        {
            if (model.Repositories.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("    ▶ [DI] No repositories detected.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("    ▶ [DI] Repository registration graph:");

            foreach (var repo in model.Repositories)
            {
                Console.WriteLine($"       {repo.Interface} -> {repo.Implementation}");
            }

            Console.ResetColor();
        }
    }

    // =====================================================
    // MODEL (IMMUTABLE)
    // =====================================================
    private sealed record RepositoryDiagnosticsModel(
        List<RepositoryInfo> Repositories
    );

    private sealed record RepositoryInfo(
        string Interface,
        string Implementation
    );
}