
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;

namespace Application;

public static class AddApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        var assembly = Assembly.GetExecutingAssembly();

        //no se necesita, services.AddScoped<Application.Features.Properties.Queries.GetPropertyQueryHandler>(); es redundante
        // Registra automáticamente todos los Commands, Queries y Handlers
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        // Aquí podrías agregar FluentValidation si lo usas
        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(Application.Common.Behaviors.ValidationBehavior<,>)
        );


        return services;

    }
}
