using Application.Features.Location.DTOs;
using Application.Features.Location.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LocationIqGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public LocationIqGeocodingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["LocationIQ:ApiKey"]
                  ?? throw new ArgumentNullException("LocationIQ ApiKey no configurada");
    }

    public async Task<List<GeocodingResponseDto>> GetCoordinatesAsync(
        string pais, string municipio, string direccion, CancellationToken ct)
    {
        var query = Uri.EscapeDataString($"{direccion} {municipio} {pais}");
        var url = $"https://us1.locationiq.com/v1/search?key={_apiKey}&q={query}&format=json&limit=10";

        using var response = await _httpClient.GetAsync(url, ct);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Error LocationIQ: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync(ct);

        var data = JsonSerializer.Deserialize<List<LocationIqResult>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new();

        return data
            .Where(x =>
                double.TryParse(x.Lat, out _) &&
                double.TryParse(x.Lon, out _))
            .Select(x => new GeocodingResponseDto
            {
                PlaceId = x.PlaceId,
                DisplayName = x.DisplayName ?? string.Empty,
                Latitude = double.Parse(x.Lat),
                Longitude = double.Parse(x.Lon)
            })
            .ToList();
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
}