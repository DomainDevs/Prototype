using Shared.Attributes;

namespace Shared.DTOs;


/// <summary>
/// DTO global/genérico para encapsular respuestas de servicios o endpoints.
/// </summary>
/// <typeparam name="T">Tipo de datos que se retorna.</typeparam>
[GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)

public class UploadFileResponse
{
    /// <summary>
    /// Nombre real del archivo almacenado en el servidor.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// URL o ruta relativa para acceder al archivo.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de archivo detectado (ej. "image/png", "application/pdf").
    /// </summary>
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// Tamaño del archivo en bytes.
    /// </summary>
    public long Size { get; set; }
}
