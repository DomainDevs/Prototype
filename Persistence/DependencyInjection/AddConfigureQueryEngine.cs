using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Persistence.DependencyInjection;

internal static class AddConfigureQueryEngine
{
    internal static IServiceCollection AddQueryEngine(this IServiceCollection services)
    {
        // Registro del QueryEngine
        services.AddScoped<IQueryEngine, Persistence.QueryEngine.QueryEngine>();

        return services;
    }
}