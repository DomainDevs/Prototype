using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;

namespace Infrastructure.Services;

/// <summary>
/// Extensión para inyectar servicios relacionados con almacenamiento de archivos.
/// </summary>
public static class ConfigureFileStorage
{
    public static IServiceCollection AddFileStorageService(this IServiceCollection services, IConfiguration config)
    {
        //services.AddScoped<IFileStorageService, FileStorageService>();
        services.Configure<UploadOptions>(
            config.GetSection("UploadOptions")
        );

        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}