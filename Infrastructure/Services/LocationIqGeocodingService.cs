using Application.Features.Location.DTOs;
using Application.Features.Location.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LocationIqGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly int _rowCount;
    private readonly string _baseUrl;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LocationIqGeocodingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _apiKey = configuration["LocationIQ:ApiKey"]
            ?? throw new ArgumentNullException(nameof(_apiKey), "LocationIQ:ApiKey no configurada");

        _baseUrl = configuration["LocationIQ:Url"]
            ?? throw new ArgumentNullException(nameof(_baseUrl), "LocationIQ:Url no configurada");

        _rowCount = int.TryParse(configuration["LocationIQ:RowCount"], out var result)
            ? result
            : 5;
    }

    public async Task<List<GeocodingResponseDto>> GetCoordinatesAsync(
        string pais,
        string municipio,
        string direccion,
        CancellationToken ct)
    {
        var query = Uri.EscapeDataString($"{direccion} {municipio} {pais}");

        var url = BuildUrl(query);

        using var response = await _httpClient.GetAsync(url, ct);
        var content = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"LocationIQ error {(int)response.StatusCode}: {content}");
        }

        var data = JsonSerializer.Deserialize<List<LocationIqResult>>(content, _jsonOptions);

        if (data == null)
            return new List<GeocodingResponseDto>();

        var result = new List<GeocodingResponseDto>();

        foreach (var item in data)
        {
            var dto = MapToDto(item);
            if (dto != null)
                result.Add(dto);
        }

        return result;
    }

    private string BuildUrl(string query)
    {
        return $"{_baseUrl}?key={_apiKey}&q={query}&format=json&limit={_rowCount}";
    }

    private static GeocodingResponseDto? MapToDto(LocationIqResult item)
    {
        if (!double.TryParse(item.Lat, NumberStyles.Any, CultureInfo.InvariantCulture, out var lat) ||
            !double.TryParse(item.Lon, NumberStyles.Any, CultureInfo.InvariantCulture, out var lon))
        {
            return null;
        }

        return new GeocodingResponseDto
        {
            PlaceId = item.PlaceId,
            DisplayName = item.DisplayName ?? string.Empty,
            Latitude = lat,
            Longitude = lon
        };
    }

    private class LocationIqResult
    {
        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; } = string.Empty;

        [JsonPropertyName("lat")]
        public string Lat { get; set; } = string.Empty;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
    }
}