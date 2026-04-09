using Microsoft.AspNetCore.Mvc;
using Application.Features.Location.DTOs;
using Application.Features.Location.Interfaces;

namespace Web.API.Controllers.Utils;

[ApiController]
[Route("api/[controller]")]
public class GeocodingController : ControllerBase
{
    private readonly IGeocodingService _geocodingService;

    public GeocodingController(IGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    [HttpPost("coordinates")]
    [ProducesResponseType(typeof(List<GeocodingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCoordinates(
        [FromBody] GeocodingRequestDto request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Municipio))
            return BadRequest("El municipio es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Direccion))
            return BadRequest("La dirección es obligatoria.");

        var result = await _geocodingService.GetCoordinatesAsync(
            request.Pais ?? "Colombia",
            request.Municipio,
            request.Direccion,
            ct
        );

        return Ok(result);
    }
}