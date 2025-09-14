using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO para mensajes de error detallados.
    /// </summary>
    [GlobalDTO]
    public class ErrorDTO
    {
        [DefaultValue(null)]
        public string? Code { get; set; } = null;

        [DefaultValue(null)]
        public string? Message { get; set; } = null;

        [DefaultValue(null)]
        public string? Field { get; set; } = null;
    }
}
