using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataToolkit.Library.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Persistence.DataToolkit;

internal static class AddConfigureDataToolkit
{
    internal static IServiceCollection AddDataToolkit(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var conStringSQl = configuration.GetConnectionString("SqlServer") ??
        throw new InvalidOperationException("Connection string 'ConnectionStrings'" +
    " not found.");

        services.AddDataToolkitSqlServer(
            configuration.GetConnectionString("SqlServer")!
        );

        services.AddScoped<IDbConnection>(sp => new SqlConnection(conStringSQl));

        return services;
    }
}