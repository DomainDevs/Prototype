// ----- Poliza_Ext -----
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("Poliza_Ext", Schema = "dbo")]
    public class PolizaExt
    {
        // Id_pv
        [Key]
        [Column("Id_pv")]
        public int IdPv { get; set; }

        // InfoAdicional
        [Column("InfoAdicional")]
        [MaxLength(400)]
        public string InfoAdicional { get; set; }

        // Observaciones
        [Column("Observaciones")]
        [MaxLength(1000)]
        public string Observaciones { get; set; }

        // ClausulasEspeciales
        [Column("ClausulasEspeciales")]
        [MaxLength(1000)]
        public string ClausulasEspeciales { get; set; }

        // DatosTecnicos
        [Column("DatosTecnicos")]
        [MaxLength(1000)]
        public string DatosTecnicos { get; set; }

    }
}
