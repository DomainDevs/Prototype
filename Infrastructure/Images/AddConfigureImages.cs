using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Images; // Un namespace más general ayuda a encontrarlo rápido

public static class ImageServiceRegistration
{
    public static IServiceCollection AddImageProcessing(this IServiceCollection services)
    {
        // Registro del procesador de imágenes
        services.AddScoped<IImageProcessor, ImageProcessor>();

        return services;
    }
}