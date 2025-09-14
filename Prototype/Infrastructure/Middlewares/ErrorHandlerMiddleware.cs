using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Middlewares;

/// <summary>
/// Middleware global para el manejo centralizado de errores en la aplicación.
/// 
/// Su responsabilidad es:
/// - Interceptar todas las excepciones no manejadas.
/// - Mapearlas a códigos HTTP apropiados (400, 401, 403, 404, 409, 500).
/// - Registrar logs con niveles adecuados (Warning o Error).
/// - Devolver respuestas JSON uniformes al cliente.
/// 
/// Este middleware es fundamental para:
/// - Evitar duplicación de try/catch en controladores o servicios.
/// - Asegurar un contrato consistente de errores para consumidores de la API.
/// - Mejorar la trazabilidad y el diagnóstico de problemas en producción.
/// </summary>
/// 
/// Beneficios de esta implementación
/// Consistencia: todos los errores siguen el mismo formato.
/// Logging claro: separa errores de negocio(400–409) de errores críticos(500).
/// Extensible: puedes añadir nuevas excepciones personalizadas en el switch.
/// Observabilidad: el traceId permite correlacionar logs con errores reportados por clientes.


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    /// <summary>
    /// Constructor del middleware de manejo de errores.
    /// </summary>
    /// <param name="next">Delegado al siguiente middleware en el pipeline.</param>
    /// <param name="logger">Logger para registrar las excepciones.</param>
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Método principal de ejecución del middleware.
    /// Captura cualquier excepción lanzada por los middlewares o controladores subsiguientes.
    /// </summary>
    /// <param name="context">El contexto HTTP actual.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Continúa con el siguiente middleware en el pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Captura y maneja cualquier excepción
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Mapea la excepción capturada a un código de estado HTTP y devuelve
    /// una respuesta JSON estandarizada al cliente.
    /// </summary>
    /// <param name="context">El contexto HTTP actual.</param>
    /// <param name="ex">La excepción capturada.</param>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        string message = ex.Message;

        // Mapeo de excepciones a códigos HTTP
        switch (ex)
        {
            case ArgumentNullException:
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest; // 400
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized; // 401
                break;

            case InvalidOperationException:
                statusCode = HttpStatusCode.Forbidden; // 403
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound; // 404
                break;

            case ApplicationException:
                statusCode = HttpStatusCode.Conflict; // 409
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError; // 500
                break;
        }

        // Logging según severidad
        if ((int)statusCode >= 500)
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
        else
            _logger.LogWarning(ex, "Handled business exception: {Message}", ex.Message);

        // Construcción de respuesta uniforme
        var response = new
        {
            statusCode = (int)statusCode,
            message,
            traceId = context.TraceIdentifier // ID de rastreo para correlación
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
