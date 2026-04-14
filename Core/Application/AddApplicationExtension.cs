using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Application.DependencyInjection;
using Application.Common.Behaviors; // 👈 Namespace agregado

namespace Application;

public static class AddApplicationExtension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        bool isDev,
        bool enableVerboseLogs = false,
        string? filter = "")
    {
        var assembly = typeof(AddApplicationExtension).Assembly;

        // 1. Registro automático de MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // 2. Registro de Validaciones (FluentValidation)
        services.AddValidatorsFromAssembly(assembly);

        // 3. Registro del Pipeline de Validación (El "filtro" de seguridad)
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>) // Ya no necesitas la ruta larga
        );

        // 4. Registro de servicios (Scrutor)
        // Diagnóstico (isDev & enableVerboseLogs y filtro por nombre filter)
        AddConfigureServices.AddServices(services); //

        return services;
    }
}