using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;

namespace Persistence.Repositories;

internal static class AddConfigureRepositories
{
    /// <summary>
    /// Registra tus repositorios de infraestructura.
    /// </summary>
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        //registra implementaciones concretas de tus repositorios
        services.AddScoped<IPropertyRepository, PropertyRepository>();

        return services;
    }

}
