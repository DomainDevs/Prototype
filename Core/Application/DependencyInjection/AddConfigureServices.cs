using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.DependencyInjection;

public static class AddConfigureServices
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        bool isDev,
        bool enableVerboseLogs = false,
        string? filter = "")
    {
        var assembly = typeof(AddConfigureServices).Assembly;

        // =========================
        // DI REGISTRATION
        // =========================
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

        // =========================
        // DIAGNOSTICS (DEV ONLY)
        // =========================
        if (isDev && enableVerboseLogs)
        {
            var model = DiagnosticsEngine.Build(assembly, filter);
            DiagnosticsRenderer.Render(model);
        }

        return services;
    }

    // =========================================================
    // ENGINE (PURE LOGIC - NO CONSOLE, NO SIDE EFFECTS)
    // =========================================================
    private static class DiagnosticsEngine
    {
        public static DiagnosticsModel Build(Assembly assembly, string? filter)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToArray(); // snapshot (performance)

            var match = CreateMatcher(filter);

            return new DiagnosticsModel(
                Filter: filter,
                Groups: new Dictionary<string, List<Type>>
                {
                    ["Services"] = Filter(types, t =>
                        IsService(t) && match(t)),

                    ["UseCases"] = Filter(types, t =>
                        t.Name.EndsWith("UseCase") && match(t)),

                    ["Handlers"] = Filter(types, t =>
                        t.Name.EndsWith("Handler") && match(t)),

                    ["Validators"] = Filter(types, t =>
                        t.Name.EndsWith("Validator") && match(t))
                }
            );
        }

        private static Func<Type, bool> CreateMatcher(string? filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return _ => true;

            return t =>
                t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase) ||
                t.Namespace?.Contains(filter!, StringComparison.OrdinalIgnoreCase) == true;
        }

        private static bool IsService(Type t)
            => t.Namespace?.Contains(".Services") == true;

        private static List<Type> Filter(Type[] types, Func<Type, bool> predicate)
            => types.Where(predicate).ToList();
    }

    // =========================================================
    // RENDERER (ONLY OUTPUT LAYER)
    // =========================================================
    private static class DiagnosticsRenderer
    {
        public static void Render(DiagnosticsModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            var mode = string.IsNullOrWhiteSpace(model.Filter)
                ? "FULL SCAN"
                : $"FILTER: '{model.Filter}'";

            Console.WriteLine($"    ▶ [DI] Application Dependency Graph ({mode})");

            foreach (var group in model.Groups)
                PrintGroup(group.Key, group.Value);

            Console.ResetColor();
        }

        private static void PrintGroup(string name, List<Type> items)
        {
            if (items.Count == 0) return;

            var tag = name switch
            {
                "Services" => "[SVC]",
                "UseCases" => "[UC]",
                "Handlers" => "[HND]",
                "Validators" => "[VAL]",
                _ => "[???]"
            };

            Console.WriteLine($"    {tag} {name}");

            foreach (var item in items.OrderBy(x => x.Name))
                Console.WriteLine($"       └─ {item.Name}");
        }
    }

    // =========================================================
    // MODEL (EXTENSIBLE + CLEAN)
    // =========================================================
    private sealed record DiagnosticsModel(
        string? Filter,
        Dictionary<string, List<Type>> Groups
    );
}