using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Auth.Jwt;

namespace Infrastructure.Auth
{
    internal static class Startup
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<JwtSettings, JwtSettings>();
            var settings = config.GetSection($"SecuritySettings:{nameof(JwtSettings)}").Get<JwtSettings>();
            services.AddJwtAuth(config, settings);

            return services;
        }
    }
}
