using Shared.DTOs;

namespace Shared.Interfaces;

/// <summary>
/// Servicio transversal de subida de archivos configurable.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Guarda un archivo aplicando validaciones de grupo (extensiones, tamaño, ruta) 
    /// definidas en la configuración.
    /// </summary>
    /// <param name="groupName">Nombre del grupo de configuración (ej. "Images", "PDF").</param>
    /// <param name="fileName">Nombre original del archivo.</param>
    /// <param name="content">Stream con el contenido del archivo.</param>
    /// <returns>Información del archivo guardado (URL, nombre seguro, etc.).</returns>
    Task<UploadFileResponse> SaveFileAsync(string groupName, string fileName, Stream content);
}