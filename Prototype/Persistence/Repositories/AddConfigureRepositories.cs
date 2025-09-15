using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using DTRepo = DataToolkit.Library.Repositories; //Alias para no generar conflicto GenericRepository

namespace Persistence.Repositories;

internal static class AddConfigureRepositories
{
    /// <summary>
    /// Archivo para proceso de inyección de repositorios, parte de infraestructura.
    /// Por lo general deben ser de tipo AddScoped, debido a que DbContext es AddScoped
    /// </summary>
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        // Registramos el GenericRepositoryAdapter<T>
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryAdapter<>));
        // Registramos también el repo real de DataToolkit
        services.AddScoped(typeof(DTRepo.IGenericRepository<>), typeof(DTRepo.GenericRepository<>));

        //registra implementaciones concretas
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();



        return services;
    }

}
