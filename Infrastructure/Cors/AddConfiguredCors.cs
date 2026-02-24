using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cors;

//la clase de Puesta en marcha, tiene como objetivo
internal static class AddConfiguredCors
{
    private const string CorsPolicy = nameof(CorsPolicy);

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var corsSettings = config.GetSection(nameof(CorsSettings)).Get<CorsSettings>() ?? new CorsSettings();
        var origins = new List<string>();

        if (!string.IsNullOrWhiteSpace(corsSettings.Blazor))
            origins.AddRange(corsSettings.Blazor.Split(';', StringSplitOptions.RemoveEmptyEntries));

        if (!string.IsNullOrWhiteSpace(corsSettings.Vue))
            origins.AddRange(corsSettings.Vue.Split(';', StringSplitOptions.RemoveEmptyEntries));

        if (!string.IsNullOrWhiteSpace(corsSettings.API))
            origins.AddRange(corsSettings.API.Split(';', StringSplitOptions.RemoveEmptyEntries));

        // Esta función limpia espacios en blanco que puedan venir del JSON
        void ParseAndAdd(string? setting)
        {
            if (!string.IsNullOrWhiteSpace(setting))
            {
                var parts = setting.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(o => o.Trim()); // <--- ESTO ES LO CLAVE
                origins.AddRange(parts);
            }
        }

        ParseAndAdd(corsSettings.Blazor);
        ParseAndAdd(corsSettings.Vue);
        ParseAndAdd(corsSettings.API);
        /*
        services.AddCors(opt =>
            opt.AddPolicy(CorsPolicy, policy =>
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins(origins.ToArray())));
        */
        services.AddCors(opt =>
            opt.AddPolicy(CorsPolicy, policy =>
                policy.WithOrigins(origins.Where(o => !string.IsNullOrWhiteSpace(o)) // Quita nulos
                                          .Select(o => o.Trim().TrimEnd('/'))        // Limpia espacios y slashes
                                          .Distinct()                               // ¡Quita los duplicados!
                                          .ToArray())
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()));

        return services;
    }

    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        return app.UseCors(CorsPolicy);
    }
}