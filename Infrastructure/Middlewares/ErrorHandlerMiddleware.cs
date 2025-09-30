using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.DTOs;
using Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

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
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //await HandleExceptionAsync(context, ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        var response = new ResponseDTO<object>
        {
            Success = false,
            Message = ex.Message,
            Data = null
        };

        // Mapeo de excepciones a códigos HTTP
        switch (ex)
        {
            case ControlValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new ResponseDTO<object>
                {
                    Success = false,
                    Message = validationEx.Message,
                    Data = validationEx.Errors
                };
                break;

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

        // Respuesta uniforme
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
