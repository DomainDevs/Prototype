using Shared.DTOs;

namespace Shared.Helpers
{
    /// <summary>
    /// Helper estático para crear respuestas uniformes y tipadas.
    /// </summary>
    public static class ApiResponse
    {
        /// <summary>
        /// Genera una respuesta exitosa.
        /// </summary>
        public static ResponseDTO<T> Success<T>(T data, string? message = null)
            => new()
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };

        /// <summary>
        /// Genera una respuesta fallida (error simple o validación).
        /// </summary>
        public static ResponseDTO<T> Fail<T>(
            string message,
            T? data = default,
            Dictionary<string, List<string>>? errors = null)
            => new()
            {
                Success = false,
                Message = message,
                Data = data,
                Errors = errors
            };
    }
}
