// ----- PolizaRiesgos -----
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("PolizaRiesgos", Schema = "dbo")]
    public class PolizaRiesgos
    {
        // Id_pv
        [Key]
        [Column("Id_pv")]
        public int IdPv { get; set; }

        // Cod_Riesgo
        [Key]
        [Column("Cod_Riesgo")]
        public int CodRiesgo { get; set; }

        // TipoRiesgo
        [Required]
        [Column("TipoRiesgo")]
        [MaxLength(200)]
        public string TipoRiesgo { get; set; }

        // Descripcion
        [Column("Descripcion")]
        [MaxLength(1000)]
        public string Descripcion { get; set; }

        // SumaAsegurada
        [Column("SumaAsegurada")]
        public decimal? SumaAsegurada { get; set; }

        // PrimaRiesgo
        [Column("PrimaRiesgo")]
        public decimal? PrimaRiesgo { get; set; }

        public ICollection<PolizaCoberturas> PolizaCoberturas { get; set; } = new List<PolizaCoberturas>();

    }
}
