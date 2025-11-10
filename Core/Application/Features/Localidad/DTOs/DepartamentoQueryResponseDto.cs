using System;
using System.Text.Json.Serialization;

namespace Application.Features.Localidad.DTOs
{
    public class DepartamentoQueryResponseDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

    }
}