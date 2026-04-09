namespace Application.Features.Location.DTOs;

public class GeocodingRequestDto
{
    public string Pais { get; set; } = "Colombia";
    public string Municipio { get; set; }
    public string Direccion { get; set; }
}
