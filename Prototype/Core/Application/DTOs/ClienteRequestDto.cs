
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ClienteRequestDto
    {
        //Identity
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

    }
}