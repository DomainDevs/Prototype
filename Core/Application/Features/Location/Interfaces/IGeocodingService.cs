using Application.Features.Location.DTOs;

namespace Application.Features.Location.Interfaces
{
    public interface IGeocodingService
    {
        Task<List<GeocodingResponseDto>> GetCoordinatesAsync(string pais, string municipio, string direccion, CancellationToken ct);
    }
}