using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cliente.DTOs
{
    public class ClienteUpdateRequestDto
    {
        [JsonIgnore]
        [JsonPropertyName("Id")]
        [Required]
        public int Id { get; set; }

        [JsonPropertyName("Email")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }


    }
}