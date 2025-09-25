using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using DataToolkit.Library.Repositories;

namespace Persistence.Repositories;

internal static class AddConfigureRepositories
{
    /// <summary>
    /// Archivo para proceso de inyección de repositorios, parte de infraestructura.
    /// Por lo general deben ser de tipo AddScoped, debido a que DbContext es AddScoped
    /// </summary>
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        //registra implementaciones concretas
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPvHeaderRepository, PvHeaderRepository>();


        return services;
    }

}
