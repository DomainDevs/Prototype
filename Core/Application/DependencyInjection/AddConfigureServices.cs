using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application.DependencyInjection;

public static class AddConfigureServices
{
    //Uso de funcionalidad Scrutor, en el arrenque permite identificar los servicios y cargarlos
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        //dotnet add package Scrutor (permite adicionar la función Scan).
        // 🔍 Escaneo automático de todos los servicios y repositorios
        var assembly = Assembly.GetExecutingAssembly();

        // Registra todos los *Service
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes
                .Where(t =>
                    t.Namespace != null &&
                    t.Namespace.StartsWith("Application.Features.") && // dentro de Features
                    t.Namespace.Contains(".Services") &&               // cualquier subcarpeta Services
                    t.Name.EndsWith("Service")                         // clases terminadas en Service
                )
            )
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        // Registrar UseCases
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c
                .Where(t => t.Namespace != null &&
                            t.Namespace.StartsWith("Application.UseCase") &&
                            t.Name.EndsWith("UseCase"))
            )
            .AsSelf()  // Se inyectan como su propia clase
            .WithScopedLifetime()
        );

        // ✅ Validación básica (opcional)
        ValidateDependencies(services);
        return services;

    }
    private static void ValidateDependencies(IServiceCollection services)
    {
        // Puedes agregar validaciones adicionales si quieres forzar contratos
        foreach (var service in services)
        {
            if (service.ServiceType.FullName?.Contains("I") == true &&
                service.ImplementationType == null)
            {
                Console.WriteLine($"⚠️ Falta implementación para {service.ServiceType.FullName}");
            }
        }
    }

}
