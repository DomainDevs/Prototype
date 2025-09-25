using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO global/genérico, para aplicar filtros y paginación en listados.
    /// </summary>
    [GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)
    public class FilterDTO
    {
        /// <summary>
        /// Término de búsqueda para filtrar resultados (opcional).
        /// </summary>
        [DefaultValue(null)]
        public string? Search { get; set; } = null;

        /// <summary>
        /// Número de página actual (por defecto 1).
        /// </summary>
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Cantidad de registros por página (por defecto 10).
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Campo por el cual se ordenarán los resultados (opcional).
        /// </summary>
        [DefaultValue(null)]
        public string? SortField { get; set; } = null;

        /// <summary>
        /// Dirección del ordenamiento: "asc" o "desc" (por defecto "asc").
        /// </summary>
        [DefaultValue("asc")]
        public string SortOrder { get; set; } = "asc";
    }
}
