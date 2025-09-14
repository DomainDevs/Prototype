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

        services.AddCors(opt =>
            opt.AddPolicy(CorsPolicy, policy =>
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins(origins.ToArray())));

        return services;
    }

    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        return app.UseCors(CorsPolicy);
    }
}