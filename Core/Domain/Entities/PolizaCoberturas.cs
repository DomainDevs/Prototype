// ----- PolizaCoberturas -----
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("PolizaCoberturas", Schema = "dbo")]
    public class PolizaCoberturas
    {
        // Id_pv
        [Key]
        [Column("Id_pv")]
        public int IdPv { get; set; }

        // Cod_Riesgo
        [Key]
        [Column("Cod_Riesgo")]
        public int CodRiesgo { get; set; }

        // Cod_Cobertura
        [Key]
        [Column("Cod_Cobertura")]
        public int CodCobertura { get; set; }

        // Cobertura
        [Required]
        [Column("Cobertura")]
        [MaxLength(400)]
        public string Cobertura { get; set; }

        // MontoAsegurado
        [Column("MontoAsegurado")]
        public decimal? MontoAsegurado { get; set; }

        // Deducible
        [Column("Deducible")]
        public decimal? Deducible { get; set; }

        // PrimaCobertura
        [Column("PrimaCobertura")]
        public decimal? PrimaCobertura { get; set; }

    }
}