using System.Text.Json.Serialization;

namespace Application.Features.Location.DTOs;

public class GeocodingResponseDto
{
    //[JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("pais")]
    public string Pais { get; set; } = "";

    [JsonPropertyName("municipio")]
    public string Municipio { get; set; } = "";

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; } = "";

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = "";

    [JsonPropertyName("placeId")]
    public string PlaceId { get; set; } = "";

    [JsonPropertyName("fechaConsulta")]
    public DateTime FechaConsulta { get; set; }

}
