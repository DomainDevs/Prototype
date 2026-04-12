using Application.Features.Upload.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers.Utils;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Endpoint para subir archivos usando un grupo configurado en el servidor.
    /// </summary>
    /// <param name="files">Archivos enviados desde el cliente</param>
    /// <param name="groupName">Grupo de configuración (ej: Images, PDF)</param>
    [HttpPost]
    [RequestSizeLimit(6_000_000)] // Aumentado a 6MB para mayor margen de maniobra
    public async Task<IActionResult> UploadFiles(
        [FromForm] IFormFileCollection files,
        [FromForm] string groupName)
    {
        if (files == null || files.Count == 0)
            return BadRequest("No se enviaron archivos.");

        if (string.IsNullOrWhiteSpace(groupName))
            return BadRequest("Debe especificarse el grupo de carga.");

        var uploadResults = new List<UploadFileResponse>();

        foreach (var file in files)
        {
            if (file.Length == 0)
                continue;

            // Abrimos el stream sin 'using'. ASP.NET Core cerrará los recursos 
            // del IFormFile automáticamente al finalizar la respuesta HTTP.
            var stream = file.OpenReadStream();

            var command = new UploadFileCommand
            {
                GroupName = groupName,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = stream,
                Size = file.Length
            };

            try
            {
                // El pipeline de MediatR ejecutará primero el UploadFileCommandValidator
                var result = await _mediator.Send(command);
                uploadResults.Add(result);
            }
            catch (Exception ex)
            {
                // Captura errores de validación, de configuración o de escritura
                uploadResults.Add(new UploadFileResponse
                {
                    FileName = file.FileName,
                    Url = string.Empty,
                    ContentType = $"Error: {ex.Message}",
                    Size = 0
                });
            }
        }

        return Ok(uploadResults);
    }
}