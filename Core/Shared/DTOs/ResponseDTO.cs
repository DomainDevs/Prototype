using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO global/genérico para encapsular respuestas de servicios o endpoints.
    /// </summary>
    /// <typeparam name="T">Tipo de datos que se retorna.</typeparam>
    [GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)
    public class ResponseDTO<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        [DefaultValue(true)]
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mensaje adicional sobre la operación, como errores o información de éxito.
        /// </summary>
        [DefaultValue(null)]
        public string? Message { get; set; } = null;

        /// <summary>
        /// Datos devueltos por la operación.
        /// </summary>
        [DefaultValue(null)]
        public T? Data { get; set; } = default;

        /// <summary>
        /// Lista detallada de errores por campo o regla de validación.
        /// Se usa principalmente para validaciones de modelo o negocio.
        /// </summary>
        [DefaultValue(null)]
        public Dictionary<string, List<string>>? Errors { get; set; } = null;
    }
}
