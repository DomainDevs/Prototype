using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Controllers.Utils;

[ApiController]
[Route("api/[controller]")]
public class GeocodingController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GeocodingController(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    /// <summary>
    /// Obtiene coordenadas geográficas a partir de una dirección.
    /// </summary>
    /// <param name="pais">País (opcional, por defecto Colombia)</param>
    /// <param name="municipio">Ciudad o municipio</param>
    /// <param name="direccion">Dirección</param>
    /// <returns>Latitud, longitud y nombre formateado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(List<GeocodingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> GetCoordinates([FromBody] GeocodingRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Municipio))
            return BadRequest("El parámetro 'municipio' es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Direccion))
            return BadRequest("El parámetro 'direccion' es obligatorio.");

        request.Pais ??= "Colombia";

        try
        {
            var query = Uri.EscapeDataString($"{request.Direccion} {request.Municipio} {request.Pais}");
            var apiKey = "pk.21cc39d91ae48ec7d7a064d2e7241480";
            Console.WriteLine(_configuration.GetSection("Connections"));
                //configuration.GetConnectionString("SqlServer")

            var url = $"https://us1.locationiq.com/v1/search?key={apiKey}&q={query}&format=json&limit=10";

            using var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Error al consultar el servicio de geocodificación.");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<List<LocationIqResult>>(json, options);

            if (data == null || !data.Any())
                return NotFound("No se encontraron resultados para la dirección proporcionada.");

            var results = data
                .Select(item =>
                {
                    if (!double.TryParse(item.Lat, out var lat) ||
                        !double.TryParse(item.Lon, out var lng))
                    {
                        return null; // descartamos inválidos
                    }

                    return new GeocodingResponseDto
                    {
                        PlaceId = item.PlaceId,
                        Latitude = lat,
                        Longitude = lng,
                        DisplayName = item.DisplayName ?? string.Empty
                    };
                })
                .Where(x => x != null)
                .ToList();

            if (!results.Any())
                return StatusCode(500, "No se pudieron procesar las coordenadas.");

            return Ok(results);
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "No fue posible comunicarse con el servicio externo.");
        }
        catch (JsonException)
        {
            return StatusCode(500, "Error al procesar la respuesta del servicio externo.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    #region DTOs
    public class GeocodingResponseDto
    {
        public string PlaceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DisplayName { get; set; }
    }

    private class LocationIqResult
    {
        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }
    #endregion

    public class GeocodingRequestDto
    {
        public string Pais { get; set; } = "Colombia";
        public string Municipio { get; set; }
        public string Direccion { get; set; }
    }

}