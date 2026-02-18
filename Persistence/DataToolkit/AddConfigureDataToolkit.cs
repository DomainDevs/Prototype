using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataToolkit.Library.Extensions;

namespace Persistence.DataToolkit;

internal static class AddConfigureDataToolkit
{
    internal static IServiceCollection AddDataToolkit(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataToolkitSqlServer(
            configuration.GetConnectionString("SqlServer")!
        );

        return services;
    }
}