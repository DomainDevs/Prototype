using System;
using System.Text.Json.Serialization;

namespace Application.Features.Cliente.DTOs
{
    public class ClienteQueryResponseDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("Apellido")]
        public string Apellido { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Ciudad")]
        public string Ciudad { get; set; }

    }
}