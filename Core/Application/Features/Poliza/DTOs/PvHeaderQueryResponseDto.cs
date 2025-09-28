using System;
using System.Text.Json.Serialization;

namespace Application.Features.Poliza.DTOs
{
    public class PvHeaderQueryResponseDto
    {
        [JsonPropertyName("IdPv")]
        public int IdPv { get; set; }

        [JsonPropertyName("CodSuc")]
        public int CodSuc { get; set; }

        [JsonPropertyName("CodRamo")]
        public int CodRamo { get; set; }

        [JsonPropertyName("NroPol")]
        public long NroPol { get; set; }

        [JsonPropertyName("NroEndoso")]
        public int NroEndoso { get; set; }

        [JsonPropertyName("CodGrupoEndo")]
        public int? CodGrupoEndo { get; set; }

        [JsonPropertyName("CodTipoEndo")]
        public int? CodTipoEndo { get; set; }

        [JsonPropertyName("TxtDescription")]
        public string TxtDescription { get; set; }

        [JsonPropertyName("FechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [JsonPropertyName("Esdigital")]
        public bool? Esdigital { get; set; }

        [JsonPropertyName("FechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("Esprueba")]
        public byte? Esprueba { get; set; }

        [JsonPropertyName("Eserror")]
        public short? Eserror { get; set; }

        [JsonPropertyName("Prima")]
        public decimal? Prima { get; set; }

        [JsonPropertyName("SumaAseg")]
        public decimal? SumaAseg { get; set; }

    }
}

