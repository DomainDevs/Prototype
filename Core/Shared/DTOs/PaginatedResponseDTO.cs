using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO global/genérico para representar respuestas paginadas de listados.
    /// </summary>
    /// <typeparam name="T">Tipo de los elementos del listado.</typeparam>
    [GlobalDTO] // Marca la clase como DTO global y reutilizable (aparece en IntelliSense y Swagger.)
    public class PaginatedResponseDTO<T>
    {
        /// <summary>
        /// Lista de elementos de la página actual.
        /// </summary>
        [DefaultValue(null)]
        public IEnumerable<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Cantidad total de registros disponibles.
        /// </summary>
        [DefaultValue(0)]
        public int TotalCount { get; set; } = 0;

        /// <summary>
        /// Número de página actual (por defecto 1).
        /// </summary>
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Tamaño de página, es decir, cantidad de registros por página (por defecto 10).
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Cantidad total de páginas calculada según TotalCount y PageSize.
        /// </summary>
        [DefaultValue(0)]
        public int TotalPages => PageSize > 0 ? (int)System.Math.Ceiling((double)TotalCount / PageSize) : 0;

        /// <summary>
        /// Indica si hay una página anterior disponible.
        /// </summary>
        [DefaultValue(false)]
        public bool HasPrevious => Page > 1;

        /// <summary>
        /// Indica si hay una página siguiente disponible.
        /// </summary>
        [DefaultValue(false)]
        public bool HasNext => Page < TotalPages;
    }
}
