// Infrastructure/DependencyInjection.cs
using Infrastructure.Cors;
using Infrastructure.Documentation;
using Infrastructure.Images;
using Infrastructure.Middlewares;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class AddInfrastructureExtension
{
    /// <summary>
    /// Registra los componentes necesario de la infraestructura
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, bool isDev = false)
    {
        if(isDev)
        services .AddOpenApiDocumentation(config);    // Configura Swagger / OpenAPI

        services
            .AddImageProcessing()             // 👈 ¡Simplemente llamas a tu método aquí!
            .AddService(config)                 // 👈 AQUÍ ESTABA FALTANDO
            .AddCorsPolicy(config)              // Configura políticas de
            .AddMemoryCache()
            .AddHttpClient();
        

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config, bool isDev = false)
    {
        // Ejecutamos la cadena base
        builder
            .UseHttpsRedirection()
            .UseErrorHandler()
            .UseRouting()
            .UseConfiguredCors()
            .UseAuthentication()
            .UseAuthorization();

        // Condicionamos Swagger
        if (isDev)
        {
            builder.UseOpenApiDocumentation(config);
        }

        return builder; // <--- Ahora el return es EXPLÍCITO y necesario
    }

}