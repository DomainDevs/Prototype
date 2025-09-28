using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Poliza.DTOs
{
    public class PvHeaderUpdateRequestDto
    {
        [JsonPropertyName("IdPv")]
        [Required]
        public int IdPv { get; set; }

        [JsonIgnore]
        [JsonPropertyName("CodSuc")]
        [Required]
        [Range(0, 999)]
        public int CodSuc { get; set; }

        [JsonIgnore]
        [JsonPropertyName("CodRamo")]
        [Required]
        [Range(0, 999)]
        public int CodRamo { get; set; }

        [JsonIgnore]
        [JsonPropertyName("NroPol")]
        [Required]
        [Range(0, 999999999999)]
        public long NroPol { get; set; }

        [JsonIgnore]
        [JsonPropertyName("NroEndoso")]
        [Required]
        [Range(0, 999999)]
        public int NroEndoso { get; set; }

        [JsonPropertyName("CodGrupoEndo")]
        [Range(0, 99)]
        public int? CodGrupoEndo { get; set; }

        [JsonPropertyName("CodTipoEndo")]
        [Range(0, 99)]
        public int? CodTipoEndo { get; set; }

        [JsonPropertyName("TxtDescription")]
        [StringLength(100)]
        public string TxtDescription { get; set; }

        [JsonPropertyName("FechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [JsonPropertyName("Esdigital")]
        public bool? Esdigital { get; set; }

        [JsonPropertyName("FechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("Esprueba")]
        [Range(0, 255)]
        public byte? Esprueba { get; set; }

        [JsonPropertyName("Eserror")]
        public short? Eserror { get; set; }

        [JsonPropertyName("Prima")]
        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal? Prima { get; set; }

        [JsonPropertyName("SumaAseg")]
        [Range(typeof(decimal), "0", "9999999999.99")]
        public decimal? SumaAseg { get; set; }

    }
}