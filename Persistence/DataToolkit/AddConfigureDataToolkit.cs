using DataToolkit.Library.Common;
using DataToolkit.Library.Connections;
using DataToolkit.Library.Extensions;
using DataToolkit.Library.UnitOfWorkLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// ⬇️ ESTE ES EL QUE TE FALTA PARA RESOLVER EL ERROR DE CONVERSIÓN
using Microsoft.Extensions.Options;
using System.Data;

namespace Persistence.DataToolkit;

internal static class AddConfigureDataToolkit
{
    internal static IServiceCollection AddDataToolkit(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var conStringSQl = configuration.GetConnectionString("SqlServer") ??
            throw new InvalidOperationException("Connection string 'SqlServer' not found.");

        // 1. Vinculamos la sección
        var section = configuration.GetSection("DataToolkit");
        services.Configure<DataToolkitOptions>(section);

        // --- BLOQUE DE VALIDACIÓN TEMPORAL ---
        var testOptions = section.Get<DataToolkitOptions>();
        Console.WriteLine("========================================");
        Console.WriteLine($"[DEBUG] Intentando cargar sección: {section.Path}");
        Console.WriteLine($"[DEBUG] ¿Sección existe?: {section.Exists()}");
        Console.WriteLine($"[DEBUG] Prefijo detectado: {testOptions?.Prefix ?? "NULO"}");
        Console.WriteLine($"[DEBUG] SlowMs detectado: {testOptions?.SlowMs.ToString() ?? "NULO"}");
        Console.WriteLine("========================================");
        // -------------------------------------

        // 2. Registramos la clase directa
        services.AddScoped(sp => sp.GetRequiredService<IOptions<DataToolkitOptions>>().Value);

        // 3. Llamada a la librería
        services.AddDataToolkitSqlServer(configuration, conStringSQl, "SqlServer");

        services.AddScoped<IDbConnection>(sp => new SqlConnection(conStringSQl));

        return services;
    }
}