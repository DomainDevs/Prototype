using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Persistence.Fluent;

namespace Persistence.DependencyInjection;

internal static class AddConfigureQueryEngine
{
    internal static IServiceCollection AddQueryEngine(this IServiceCollection services)
    {
        // Registro del QueryEngine
        services.AddScoped<IQueryEngine, QueryEngine>();

        return services;
    }
}