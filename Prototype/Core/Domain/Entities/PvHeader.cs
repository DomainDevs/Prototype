using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("pv_header", Schema = "dbo")]
    public class PvHeader
    {
        // id_pv
        [Column("id_pv")]
        public int IdPv { get; set; }

        // cod_suc
        [Key]
        [Column("cod_suc")]
        public int CodSuc { get; set; }

        // cod_ramo
        [Key]
        [Column("cod_ramo")]
        public int CodRamo { get; set; }

        // nro_pol
        [Key]
        [Column("nro_pol")]
        public long NroPol { get; set; }

        // nro_endoso
        [Key]
        [Column("nro_endoso")]
        public int NroEndoso { get; set; }

        // cod_grupo_endo
        [Column("cod_grupo_endo")]
        public int? CodGrupoEndo { get; set; }

        // cod_tipo_endo
        [Column("cod_tipo_endo")]
        public int? CodTipoEndo { get; set; }

        // txt_description
        [Column("txt_description")]
        [MaxLength(100)]
        public string TxtDescription { get; set; }

        // Fecha_Vencimiento
        [Column("Fecha_Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        // esdigital
        [Column("esdigital")]
        public bool? Esdigital { get; set; }

        // fecha_creacion
        [Column("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }

        // esprueba
        [Column("esprueba")]
        public byte? Esprueba { get; set; }

        // eserror
        [Column("eserror")]
        public short? Eserror { get; set; }

        // prima
        [Column("prima")]
        public decimal? Prima { get; set; }

        // suma_aseg
        [Column("suma_aseg")]
        public decimal? SumaAseg { get; set; }

    }
}