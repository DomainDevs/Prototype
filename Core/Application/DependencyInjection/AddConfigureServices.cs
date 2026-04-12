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

        // 🚀 Registro con Scrutor: Solo para lo que NO es MediatR/FluentValidation
        services.Scan(scan => scan
            .FromAssemblies(assembly)

            // 1. Servicios tradicionales (clases en carpetas .Services)
            .AddClasses(c => c.InNamespaces("Application.Features")
                .Where(t => t.Namespace != null && t.Namespace.Contains(".Services")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // 2. UseCases (clases que terminan en UseCase)
            .AddClasses(c => c.InNamespaces("Application.UseCase")
                .Where(t => t.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithScopedLifetime()
        );

        // 🔍 Diagnóstico inteligente
        if (isDev && enableVerboseLogs)
        {
            PrintApplicationDetails(assembly, filter);
        }

        return services;
    }

    private static void PrintApplicationDetails(Assembly assembly, 
        string? filter = "") //filtro de cuales mostrar
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();

        // Determinamos si aplicamos filtro visual
        bool hasFilter = !string.IsNullOrWhiteSpace(filter);

        // Categorizamos los tipos
        var services = types.Where(t => t.Namespace?.Contains(".Services") == true);
        var useCases = types.Where(t => t.Name.EndsWith("UseCase"));
        var handlers = types.Where(t => t.Name.EndsWith("Handler"));
        var validators = types.Where(t => t.Name.EndsWith("Validator"));

        // Aplicamos el filtro de texto solo si el usuario lo proporcionó
        if (hasFilter)
        {
            services = services.Where(t => t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase));
            useCases = useCases.Where(t => t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase));
            handlers = handlers.Where(t => t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase));
            validators = validators.Where(t => t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase));
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        string mode = hasFilter ? $"Filtrando por: '{filter}'" : "Listado completo";
        Console.WriteLine($"    🔍 [DI] Diagnóstico de Aplicación ({mode}):");

        void PrintGroup(string label, string icon, IEnumerable<Type> items)
        {
            var list = items.OrderBy(n => n.Name).ToList();
            if (!list.Any()) return;

            Console.WriteLine($"    {icon} {label}:");
            foreach (var item in list)
                Console.WriteLine($"       → {item.Name}");
        }

        PrintGroup("Servicios", "⚙️ ", services);
        PrintGroup("UseCases", "⚡", useCases);
        PrintGroup("Handlers (MediatR)", "📨", handlers);
        PrintGroup("Validadores", "🛡️ ", validators);

        Console.ResetColor();
    }
}