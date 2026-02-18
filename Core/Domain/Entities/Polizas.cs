// ----- Polizas -----
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("Polizas", Schema = "dbo")]
    public class Polizas
    {
        // Id_pv
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id_pv")]
        public int IdPv { get; set; }

        // NumeroPoliza
        [Required]
        [Column("NumeroPoliza")]
        [MaxLength(100)]
        public string NumeroPoliza { get; set; }

        // FechaEmision
        [Column("FechaEmision")]
        public DateTime FechaEmision { get; set; }

        // FechaInicio
        [Column("FechaInicio")]
        public DateTime FechaInicio { get; set; }

        // FechaFin
        [Column("FechaFin")]
        public DateTime FechaFin { get; set; }

        // AseguradoId
        [Column("AseguradoId")]
        public int AseguradoId { get; set; }

        // SumaAsegurada
        [Column("SumaAsegurada")]
        public decimal? SumaAsegurada { get; set; }

        // PrimaTotal
        [Column("PrimaTotal")]
        public decimal? PrimaTotal { get; set; }

        // Estado
        [Column("Estado")]
        [MaxLength(100)]
        public string Estado { get; set; }

        public PolizaExt? PolizaExt { get; set; }

        public ICollection<PolizaRiesgos> PolizaRiesgos { get; set; } = new List<PolizaRiesgos>();

    }
}
