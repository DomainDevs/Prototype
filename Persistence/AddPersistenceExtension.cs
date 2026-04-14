using DataToolkit.Library.Repositories;
using DataToolkit.Library.Sql;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataToolkit;
using Persistence.DependencyInjection;

namespace Persistence;

public static class AddPersistenceExtension
{
    /// <summary>
    /// Registra DataToolkit (librería de conexión) y tus repositorios.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration config,
        bool isDev,
        bool enableVerboseLogs = false
        )
    {
        // 1. Configuración de motor/conexión
        services.AddDataToolkit(config);

        // 2. Registro de Repositorios 
        services.AddRepositories(); //enableVerboseLogs

        return services;
    }

}
