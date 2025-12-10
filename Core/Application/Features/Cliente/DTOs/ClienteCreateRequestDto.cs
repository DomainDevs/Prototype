using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cliente.DTOs
{
    public class ClienteCreateRequestDto
    {
        [JsonIgnore]
        [JsonPropertyName("Id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("Nombre")]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [JsonPropertyName("Apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [JsonPropertyName("Email")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [JsonPropertyName("Ciudad")]
        [StringLength(200)]
        public string Ciudad { get; set; }

    }
}
