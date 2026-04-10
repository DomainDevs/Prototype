using Application.Features.Location.DTOs;
using Application.Features.Location.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class LocationIqGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LocationIqGeocodingService> _logger;
    private readonly IMemoryCache _cache;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LocationIqGeocodingService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<LocationIqGeocodingService> logger,
        IMemoryCache cache)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        _cache = cache;
    }

    public async Task<List<GeocodingResponseDto>> GetCoordinatesAsync(
        string pais,
        string municipio,
        string direccion,
        CancellationToken ct)
    {
        var cacheKey = $"{pais}-{municipio}-{direccion}".ToLowerInvariant();

        if (_cache.TryGetValue(cacheKey, out List<GeocodingResponseDto> cached))
        {
            _logger.LogInformation("Cache hit for {CacheKey}", cacheKey);
            return cached;
        }

        var apiKey = GetRequired("LocationIQ:ApiKey");
        var baseUrl = GetRequired("LocationIQ:Url");
        var limit = GetInt("LocationIQ:RowCount", 5);

        var url = BuildUrl(baseUrl, apiKey, pais, municipio, direccion, limit);

        var start = DateTime.UtcNow;

        try
        {
            _logger.LogInformation("LocationIQ request started: {Url}", url);

            using var response = await SendWithRetryAsync(url, ct);
            var content = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("LocationIQ error {Status}: {Body}",
                    (int)response.StatusCode, content);

                throw new HttpRequestException($"LocationIQ error {(int)response.StatusCode}");
            }

            var result = Parse(content);

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

            _logger.LogInformation(
                "LocationIQ success in {Time}ms, results: {Count}",
                (DateTime.UtcNow - start).TotalMilliseconds,
                result.Count);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LocationIQ failed");
            throw;
        }
    }

    // =========================
    // HTTP WITH RETRY (simple, no Polly)
    // =========================
    private async Task<HttpResponseMessage> SendWithRetryAsync(
        string url,
        CancellationToken ct)
    {
        const int maxRetries = 2;

        for (int i = 0; i <= maxRetries; i++)
        {
            try
            {
                var response = await _httpClient.GetAsync(url, ct);

                if (response.IsSuccessStatusCode)
                    return response;

                // retry only on transient errors
                if ((int)response.StatusCode >= 500 && i < maxRetries)
                {
                    await Task.Delay(300 * (i + 1), ct);
                    continue;
                }

                return response;
            }
            catch when (i < maxRetries)
            {
                await Task.Delay(300 * (i + 1), ct);
            }
        }

        throw new HttpRequestException("Max retry attempts reached");
    }

    // =========================
    // PARSE
    // =========================
    private List<GeocodingResponseDto> Parse(string json)
    {
        var data = JsonSerializer.Deserialize<List<LocationIqResult>>(json, JsonOptions)
                   ?? [];

        var result = new List<GeocodingResponseDto>();

        foreach (var item in data)
        {
            if (TryMap(item, out var dto))
                result.Add(dto);
        }

        return result;
    }

    // =========================
    // MAPPING
    // =========================
    private static bool TryMap(LocationIqResult item, out GeocodingResponseDto dto)
    {
        dto = null!;

        if (!double.TryParse(item.Lat, NumberStyles.Any, CultureInfo.InvariantCulture, out var lat) ||
            !double.TryParse(item.Lon, NumberStyles.Any, CultureInfo.InvariantCulture, out var lon))
            return false;

        dto = new GeocodingResponseDto
        {
            PlaceId = item.PlaceId,
            DisplayName = item.DisplayName ?? string.Empty,
            Latitude = lat,
            Longitude = lon
        };

        return true;
    }

    // =========================
    // URL BUILDER
    // =========================
    private static string BuildUrl(
        string baseUrl,
        string apiKey,
        string pais,
        string municipio,
        string direccion,
        int limit)
    {
        var query = Uri.EscapeDataString($"{direccion} {municipio} {pais}");

        return $"{baseUrl}?key={apiKey}&q={query}&format=json&limit={limit}";
    }

    // =========================
    // CONFIG HELPERS
    // =========================
    private string GetRequired(string key)
        => _configuration[key]
           ?? throw new ArgumentNullException(key);

    private int GetInt(string key, int defaultValue)
        => int.TryParse(_configuration[key], out var val) ? val : defaultValue;

    // =========================
    // MODEL
    // =========================
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