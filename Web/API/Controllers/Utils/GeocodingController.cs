using Application.Features.Location.DTOs;
using Application.Features.Location.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Utils;

[ApiController]
[Route("api/[controller]")]
public class GeocodingController : ControllerBase
{
    private readonly IGeocodingService _geocodingService;

    public GeocodingController(IGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<GeocodingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCoordinates(
        [FromBody] GeocodingRequestDto request,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        if (string.IsNullOrWhiteSpace(request?.Municipio))
            return BadRequest("El campo 'municipio' es obligatorio.");

        if (string.IsNullOrWhiteSpace(request?.Direccion))
            return BadRequest("El campo 'direccion' es obligatorio.");

        request.Pais ??= "Colombia";

        var result = await _geocodingService.GetCoordinatesAsync(
            request.Pais,
            request.Municipio,
            request.Direccion,
            ct);

        if (result is null || result.Count == 0)
            return NotFound("No se encontraron resultados para la dirección proporcionada.");

        return Ok(result);
    }
}