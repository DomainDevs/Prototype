using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Prototype.Infrastructure.Persistence
{
    internal static class Startup
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            // TODO: there must be a cleaner way to do IOptions validation...
            var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            string? rootConnectionString = databaseSettings.ConnectionString;

            return null;
        }
    }
}
