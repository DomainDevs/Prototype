using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Localidad.DTOs
{
    public class DepartamentoUpdateRequestDto
    {
        [JsonIgnore]
        [JsonPropertyName("Id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("Nombre")]
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

    }
}