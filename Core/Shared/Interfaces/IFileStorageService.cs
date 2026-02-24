using Shared.DTOs;

namespace Shared.Interfaces;

/// <summary>
/// Servicio transversal de subida de archivos configurable.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Guarda un archivo con validaciones personalizadas según los parámetros.
    /// </summary>
    /// <param name="fileName">Nombre del archivo a almacenar.</param>
    /// <param name="content">Contenido en bytes.</param>
    /// <param name="folderPath">Ruta relativa donde guardar el archivo.</param>
    /// <param name="allowedExtensions">Lista de extensiones permitidas (ej. ".jpg", ".pdf").</param>
    /// <param name="maxFileSizeBytes">Tamaño máximo permitido en bytes.</param>
    /// <returns>Información del archivo guardado.</returns>
    Task<UploadFileResponse> SaveFileAsync(string groupName, string fileName, byte[] content);
}