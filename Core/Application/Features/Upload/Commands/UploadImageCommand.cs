using MediatR;
using Shared.DTOs;

namespace Application.Features.Upload.Commands;

/// <summary>
/// Comando para subir cualquier archivo usando un grupo de configuración.
/// </summary>
public class UploadFileCommand : IRequest<UploadFileResponse>
{
    /// <summary>Nombre del grupo de configuración definido en upload.json (ej: "Images", "PDF")</summary>
    public string GroupName { get; set; } = string.Empty;

    /// <summary>Nombre del archivo a subir</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>Contenido del archivo</summary>
    public byte[] Content { get; set; } = Array.Empty<byte>();
}