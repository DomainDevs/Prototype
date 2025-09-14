using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO para subir archivos.
    /// </summary>
    [GlobalDTO]
    public class FileUploadDTO
    {
        [DefaultValue(null)]
        public string? FileName { get; set; } = null;

        [DefaultValue(null)]
        public byte[]? FileContent { get; set; } = null;

        [DefaultValue(null)]
        public string? ContentType { get; set; } = null;
    }
}
