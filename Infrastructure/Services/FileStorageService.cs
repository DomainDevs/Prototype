using Microsoft.AspNetCore.Hosting;
using Shared.Interfaces;
using Shared.DTOs;
using Shared.Helpers;
using Microsoft.Extensions.Options;
using Infrastructure.Configuration;
using Infrastructure.Images;

namespace Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly UploadOptions _options;
    private readonly IImageProcessor _imageProcessor;

    public FileStorageService(
        IWebHostEnvironment env,
        IOptions<UploadOptions> options,
        IImageProcessor imageProcessor) // Inyectamos el nuevo procesador
    {
        _env = env;
        _options = options.Value;
        _imageProcessor = imageProcessor;
    }

    public async Task<UploadFileResponse> SaveFileAsync(string groupName, string fileName, Stream content)
    {
        // 1. Validar si el grupo existe
        if (!_options.UploadGroups.TryGetValue(groupName, out var group))
            throw new ArgumentException($"El grupo '{groupName}' no existe.");

        // 2. Validación de extensión original (seguridad previa a la conversión)
        var originalExtension = Path.GetExtension(fileName).ToLower();
        if (string.IsNullOrEmpty(originalExtension) || !group.AllowedExtensions.Contains(originalExtension))
            throw new ArgumentException($"Extensión '{originalExtension}' no permitida para este grupo.");

        // 3. Procesamiento y conversión a WebP
        // El procesador se encarga de la lógica pesada de SkiaSharp
        byte[] webpData = await _imageProcessor.ConvertToWebpAsync(content);

        // 4. Preparar rutas y nombres (Forzamos .webp)
        var safeFileName = $"{GuidExtensions.GenerateShortId()}.webp";
        var folder = Path.Combine(_env.WebRootPath ?? "wwwroot", group.FolderPath);

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, safeFileName);

        // 5. Guardar los bytes procesados en disco
        await File.WriteAllBytesAsync(fullPath, webpData);

        // 6. Retornar respuesta
        return new UploadFileResponse
        {
            FileName = safeFileName,
            Url = Path.Combine("/", group.FolderPath, safeFileName).Replace("\\", "/"),
            ContentType = "image/webp", // Siempre es webp
            Size = webpData.Length      // El tamaño real de la imagen optimizada
        };
    }
}