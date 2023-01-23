using Prototype.Infrastructure.Cors;
using Prototype.Infrastructure.Documentation;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            if (Convert.ToBoolean(config.GetSection("DocumentationEnabled").Value))
                services.AddOpenApiDocumentation(config);

            return services
            .AddAuth(config)
            .AddLocalization()
            .AddCorsPolicy(config);
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>


            builder
                .UseOpenApiDocumentation(config)
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseCorsPolicy();
    }
}
