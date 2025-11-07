using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataToolkit.Library.Extensions;
using DataToolkit.Library.Sql;
using DataToolkit.Library.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Persistence.DataToolkit;

internal static class AddConfigureDataToolkit
{
    /// <summary>
    /// Registra las conexiones a una o más bases de datos. DataToolkit con SQL Server /Sybase
    /// </summary>
    internal static IServiceCollection AddDataToolkit(
        this IServiceCollection services, 
        IConfiguration configuration)
    {

        var conStringSQl = configuration.GetConnectionString("SqlServer") ??
                throw new InvalidOperationException("Connection string 'ConnectionStrings'" +
            " not found.");

        // Registrar IDbConnection (scoped, igual que los repositorios)
        services.AddScoped<IDbConnection>(sp => new SqlConnection(conStringSQl));
        // Si quieres inyectar ISqlExecutor también
        services.AddScoped<ISqlExecutor>(sp => sp.GetRequiredService<SqlExecutor>());
        //Conexion simple una BD
        services.AddDataToolkitSqlServer(configuration.GetConnectionString("SqlServer")!);

        #region multipleBD
        //Conexion multiples BD
        /*
        var conSybase = configuration.GetConnectionString("Sybase") ??
            throw new InvalidOperationException("Connection string 'ConnectionStrings'" +
            " not found.");

        services.AddDataToolkitWith(options =>
        {
            options.AddConnection("SqlServer", conStringSQl, DatabaseProvider.SqlServer);
            //options.AddConnection("Sybase", conSybase, DatabaseProvider.Sybase);
            options.DefaultAlias = "SqlServer";
        });
        */
        #endregion

        return services;
    }

}