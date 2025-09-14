using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO global/genérico, para representar pares clave-valor.
    /// </summary>
    [GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)
    public class KeyValueDTO
    {
        /// <summary>
        /// Clave del par.
        /// </summary>
        [DefaultValue(null)]
        public string? Key { get; set; } = null;

        /// <summary>
        /// Valor del par.
        /// </summary>
        [DefaultValue(null)]
        public string? Value { get; set; } = null;
    }
}
