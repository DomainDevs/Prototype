using MediatR;
using Shared.DTOs;

namespace Application.Features.Upload.Commands;

/// <summary>
/// Comando para subir cualquier archivo usando un grupo de configuración.
/// </summary>
public class UploadImageCommand : IRequest<UploadFileResponse>
{
    /// <summary>Nombre del grupo de configuración definido en upload.json (ej: "Images", "PDF")</summary>
    public string GroupName { get; set; } = string.Empty;

    /// <summary>Nombre del archivo a subir</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>Tipo MIME (image/jpeg, image/png, etc.)</summary>
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// Stream del archivo (más eficiente que byte[])
    /// </summary>
    public Stream Content { get; set; } = default!;

    /// <summary>
    /// Tamaño archivo
    /// </summary>
    public long Size { get; set; }
}