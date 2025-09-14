using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO global/genérico, para representar rangos de fechas.
    /// </summary>
    [GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)
    public class DateRangeDTO
    {
        /// <summary>
        /// Fecha inicial del rango.
        /// </summary>
        [DefaultValue(null)]
        public DateTime? From { get; set; } = null;

        /// <summary>
        /// Fecha final del rango.
        /// </summary>
        [DefaultValue(null)]
        public DateTime? To { get; set; } = null;
    }
}
