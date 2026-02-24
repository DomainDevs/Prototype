using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Shared.Interfaces;
using Shared.DTOs;
using Microsoft.Extensions.Options;
using Infrastructure.Configuration;

namespace Infrastructure.Services;

/// <summary>
/// Servicio de almacenamiento seguro de archivos en el servidor.
/// Valida extensiones, tamaño, existencia de carpeta y nombre seguro.
/// </summary>
public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly UploadOptions _options;

    public FileStorageService(IWebHostEnvironment env, IOptions<UploadOptions> options)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<UploadFileResponse> SaveFileAsync(string groupName, string fileName, byte[] content)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("El nombre del grupo no puede estar vacío.", nameof(groupName));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(fileName));

        if (!_options.UploadGroups.TryGetValue(groupName, out var group))
            throw new ArgumentException($"El grupo '{groupName}' no existe en la configuración.");

        // Validar extensión
        var ext = Path.GetExtension(fileName)?.ToLower();
        if (string.IsNullOrEmpty(ext) || !group.AllowedExtensions.Contains(ext))
            throw new ArgumentException($"Extensión '{ext}' no permitida para el grupo '{groupName}'.");

        // Validar tamaño
        if (content.Length > group.MaxFileSizeBytes)
            throw new InvalidOperationException(
                $"El archivo supera el tamaño máximo permitido ({group.MaxFileSizeBytes} bytes).");

        // Carpeta segura
        var folder = Path.Combine(_env.WebRootPath ?? "", group.FolderPath);
        try
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"No se pudo crear la carpeta '{folder}': {ex.Message}", ex);
        }

        // Nombre de archivo seguro
        var safeFileName = Path.GetFileName(fileName);
        var fullPath = Path.Combine(folder, safeFileName);

        try
        {
            await File.WriteAllBytesAsync(fullPath, content);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error guardando archivo '{safeFileName}' en '{folder}': {ex.Message}", ex);
        }

        // Determinar content type
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(safeFileName, out var contentType))
            contentType = "application/octet-stream";

        // Construir URL
        var url = Path.Combine("/", group.FolderPath.Replace("\\", "/"), safeFileName);

        return new UploadFileResponse
        {
            FileName = safeFileName,
            Url = url,
            ContentType = contentType,
            Size = content.Length
        };
    }
}