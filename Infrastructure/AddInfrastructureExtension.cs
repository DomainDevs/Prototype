// Infrastructure/DependencyInjection.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Documentation;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Infrastructure.Cors;
using Infrastructure.Middlewares;


namespace Infrastructure;

public static class AddInfrastructureExtension
{
    /// <summary>
    /// Registra los componentes necesario de la infraestructura
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            
            .AddOpenApiDocumentation(config)    // Configura Swagger / OpenAPI
            .AddFileStorageService(config)   // 👈 AQUÍ ESTABA FALTANDO
            .AddCorsPolicy(config);             // Configura políticas de CORS

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>

        //Rcomendación Microsoft y OWASP
        builder
            .UseHttpsRedirection()              // 1. Fuerza HTTPS
            .UseErrorHandler()                  // 2. ✅ Manejo global de errores
            .UseRouting()                       // 3. Middleware de routing
            .UseCors()                          // 4. CORS antes de Auth: Middleware de CORS
            .UseAuthentication()                // 5. Auth: Middleware de autenticación
            .UseAuthorization()                 // 6. Authorization: Middleware de autorización
            .UseOpenApiDocumentation(config);    // 7. Swagger (último de los middlewares extra)

}
