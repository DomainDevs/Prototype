using DataToolkit.Library.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataToolkit;
using Persistence.Repositories;

namespace Persistence;

public static class AddPersistenceExtension
{
    /// <summary>
    /// Registra DataToolkit con SQL Server y tus repositorios.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        //Si quieres inyectar por interfaz en vez de tipo de conexion concreto
        services.AddScoped<ISqlExecutor>(sp => sp.GetRequiredService<SqlExecutor>());

        services
            .AddDataToolkit(config)
            .AddRepositories();

        return services;
    }

}
